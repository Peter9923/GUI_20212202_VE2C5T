using GameLogic;
using GameModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Game
{
    public class Display : FrameworkElement
    {
        //Brushes
        private Brush KnightBrush;
        private Brush EnemyKnightBrush;
        //Button Brushes
        private Brush DeployKnightBrush;
        private Brush DeployKnightSelectedBrush;
        private Brush MoveButtonBrush;
        private Brush MoveButtonSelectedBrush;
        private Brush RemoveButtonBrush;
        private Brush RemoveButtonSelectedBrush;
        private Brush UpgradeButtonBrush;
        private Brush UpgradeButtonSelectedBrush;

        //Backround Brushes
        private Brush GrassBrush;
        private Brush EnemyGrassBrush;
        private Brush CastleWallBrush;


        private static Random rnd = new Random();

        private IModel model;
        private ILogic logic;
        int backgroundRendererCount = 0;

        private Window win;

        private DispatcherTimer EnemySpawnTimer;

        Point MovedMouseTilePos;
        Point PrevMovedMouseTilePos;


        Point MovedUnitPrevPos;

       


        public Display()
        {
            KnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Knight.png", UriKind.RelativeOrAbsolute)));
            EnemyKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\EnemyKnight.png", UriKind.RelativeOrAbsolute)));

            DeployKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnight.png", UriKind.RelativeOrAbsolute)));
            DeployKnightSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnightSelected.png", UriKind.RelativeOrAbsolute)));
            MoveButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Move.png", UriKind.RelativeOrAbsolute)));
            MoveButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\MoveSelected.png", UriKind.RelativeOrAbsolute)));
            RemoveButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Remove.png", UriKind.RelativeOrAbsolute)));
            RemoveButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\RemoveSelected.png", UriKind.RelativeOrAbsolute)));
            UpgradeButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Upgrade.png", UriKind.RelativeOrAbsolute)));
            UpgradeButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\UpgradeSelected.png", UriKind.RelativeOrAbsolute)));

            GrassBrush = new ImageBrush(new BitmapImage(new Uri("Images\\grass.png", UriKind.RelativeOrAbsolute)));
            EnemyGrassBrush = new ImageBrush(new BitmapImage(new Uri("Images\\EnemyGrass.png", UriKind.RelativeOrAbsolute)));
            CastleWallBrush = new ImageBrush(new BitmapImage(new Uri("Images\\CastleWall.png", UriKind.RelativeOrAbsolute)));


            this.Loaded += this.CastleDefendersControl_Loaded;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (drawingContext != null){
                if (this.model != null){
                    if (backgroundRendererCount == 0){
                        DrawBackgroundElements(drawingContext);
                    }

                    DrawButtons(drawingContext);
                    DrawKnights(drawingContext);
                    DrawEnemies(drawingContext);
                    DrawForegroundElements(drawingContext);


                    //borders
                    //..
                    if ((int)MovedMouseTilePos.Y <= this.model.Map.Length - 1
                            && (int)MovedMouseTilePos.X <= this.model.Map[0].Length - 2
                            && (int)MovedMouseTilePos.Y >= 0
                            && (int)MovedMouseTilePos.X >= 0)
                    {
                        if (this.model.Map[(int)MovedMouseTilePos.Y][(int)MovedMouseTilePos.X] == null)
                        {
                            if (this.model.DeployKnight)
                            {
                                drawingContext.DrawGeometry(this.KnightBrush, new Pen(Brushes.Black, 8), new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));

                            }
                            else if (this.model.MoveUnit)
                            {
                                DrawBorder(drawingContext);
                            }
                        }
                        else
                        { //mouse is an exist object.
                            DrawBorder(drawingContext);
                        }
                        DrawBorder(drawingContext);

                    }
                    //..


                }
            }
        }

        private void DrawBackgroundElements(DrawingContext drawingContext)
        {
            DrawCellBackground(drawingContext);
            DrawEnemyCellBackground(drawingContext);
            DrawCastleWall(drawingContext);
            DrawCastleHpBoxBackground(drawingContext);
            DrawCastleHpText(drawingContext);
            DrawGoldText(drawingContext);
        }

        #region Not Changing Elements
        private void DrawCellBackground(DrawingContext drawingContext)
        {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length - 1; x++)
                {
                    Geometry box = new RectangleGeometry(new Rect((x + 1) * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                    drawingContext.DrawGeometry(this.GrassBrush, null, box);
                }
            }

        }
        private void DrawEnemyCellBackground(DrawingContext drawingContext)
        {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                Geometry box = new RectangleGeometry(new Rect(10 * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                drawingContext.DrawGeometry(this.EnemyGrassBrush, null, box);
            }
        }
        private void DrawCastleWall(DrawingContext drawingContext)
        {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                Geometry box = new RectangleGeometry(new Rect(0 * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                drawingContext.DrawGeometry(this.CastleWallBrush, null, box);
            }
        }
        private void DrawCastleHpBoxBackground(DrawingContext drawingContext)
        {
            Geometry rect1 = new RectangleGeometry(new Rect(180, 825, 300, 30));
            drawingContext.DrawGeometry(Brushes.WhiteSmoke, null, rect1);
        }

        private void DrawCastleHpText(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText("Castle HP: ", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(20, 825)));
        }

        private void DrawGoldText(DrawingContext drawingContext)
        {
            FormattedText text1 = new FormattedText("Gold: ", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 2);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text1.BuildGeometry(new Point(500, 825)));
        }
        #endregion

        #region DrawButtons
        private void DrawButtons(DrawingContext drawingContext) {
            DeployKnightButton(drawingContext);
            MoveButton(drawingContext);
            RemoveButton(drawingContext);
            UpgradeButton(drawingContext);
        }
        private void DeployKnightButton(DrawingContext drawingContext) {
            Geometry g = new RectangleGeometry(new Rect(
                   (this.model.Map[0].Length + 1) * Config.TileSize, 0 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.DeployKnight)
            {
                drawingContext.DrawGeometry(this.DeployKnightSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.DeployKnightBrush, null, g);
            }
        }
        private void MoveButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length - 1) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.MoveUnit)
            {
                drawingContext.DrawGeometry(this.MoveButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.MoveButtonBrush, null, g);
            }
        }
        private void RemoveButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.RemoveUnit)
            {
                drawingContext.DrawGeometry(this.RemoveButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.RemoveButtonBrush, null, g);
            }
        }
        private void UpgradeButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length - 2) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.UpgradeUnit)
            {
                drawingContext.DrawGeometry(this.UpgradeButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.UpgradeButtonBrush, null, g);
            }
        }
        #endregion

        #region Foreground Elements

        private void DrawForegroundElements(DrawingContext drawingContext)
        {
            DrawCastleHpBoxForeground(drawingContext);
            DrawCurrentCastleHp(drawingContext);
            DrawCurrentGold(drawingContext);
            DrawWave(drawingContext);
            DrawScore(drawingContext);
            DrawHpsBackground(drawingContext);
            DrawHpsForeground(drawingContext);
            DrawLevels(drawingContext);
        }
        private void DrawCastleHpBoxForeground(DrawingContext drawingContext)
        {
            Geometry rect2 = new RectangleGeometry(new Rect(180, 825, ((300 * this.model.CastleActualHP) / this.model.CastleMaxHP), 30));
            drawingContext.DrawGeometry(Brushes.DarkRed, null, rect2);
        }
        private void DrawCurrentCastleHp(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText(this.model.CastleActualHP.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen((this.model.CastleActualHP >= 150 ? Brushes.DarkGreen : Brushes.DarkRed), 2), text.BuildGeometry(new Point(310, 825)));
        }

        private void DrawCurrentGold(DrawingContext drawingContext)
        {
            FormattedText text1 = new FormattedText(this.model.Gold.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Yellow, 2);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Yellow, 2), text1.BuildGeometry(new Point(580, 825)));
        }
        private void DrawWave(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText("Wave: " + this.model.Wave.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(20, 900)));
        }

        private void DrawScore(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText("Score: " + this.model.Score.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(500, 900)));
        }

        private void DrawLevels(DrawingContext drawingContext)
        {
            FormattedText text;
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied)
                    {
                        text = new FormattedText(this.model.Map[y][x].Level.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 45, Brushes.Black, 1);
                        drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.WhiteSmoke, 1), text.BuildGeometry(new Point((x + 1) * Config.TileSize, y * Config.TileSize)));
                    }
                }
            }
        }

        private void DrawHpsBackground(DrawingContext drawingContext)
        {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied)
                    {
                        Geometry rect1 = new RectangleGeometry(new Rect((x + 1) * Config.TileSize + 50, y * Config.TileSize, 80, 15));
                        drawingContext.DrawGeometry(Brushes.WhiteSmoke, null, rect1);
                    }
                }
            }
        }

        private void DrawHpsForeground(DrawingContext drawingContext)
        {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied actual)
                    {
                        Geometry rect1 = new RectangleGeometry(new Rect((x + 1) * Config.TileSize + 50, y * Config.TileSize, ((80 * actual.ActualLife) / actual.MaxLife), 15));
                        drawingContext.DrawGeometry(Brushes.DarkRed, null, rect1);
                    }
                }
            }
        }

        #endregion

        private void DrawKnights(DrawingContext drawingContext) {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is Knight)
                    {
                        drawingContext.DrawGeometry(this.KnightBrush, null, this.model.Map[y][x].Area);
                    }
                }
            }
        }
        private void DrawEnemies(DrawingContext drawingContext)
        {
            foreach (var item in this.model.SpawnedEnemies)
            {
                drawingContext.DrawGeometry(this.EnemyKnightBrush, null, item.Area);
            }
        }


        private void DrawBorder(DrawingContext drawingContext)
        {
            Pen actual = new Pen();
            if (this.model.RemoveUnit)
            {
                actual = new Pen(Brushes.Red, 8);
            }
            else if (this.model.MoveUnit)
            {
                actual = new Pen(Brushes.Aqua, 8);

            }
            else if (this.model.UpgradeUnit)
            {
                actual = new Pen(Brushes.Purple, 8);

            }
            else
            {
                actual = new Pen(Brushes.Yellow, 8);

            }
            drawingContext.DrawGeometry(null, actual, new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
        }
        







        private void CastleDefendersControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.win = Window.GetWindow(this);
            this.model = new Model(this.ActualHeight, this.ActualWidth);
            this.logic = new Logic(this.model);
            this.win = Window.GetWindow(this);

            MovedUnitPrevPos = new Point(-1, -1);

            if (this.win != null)
            {
                this.MouseDown += Display_MouseDown;
                this.MouseMove += Display_MouseMove;

                this.SetTimers();
            }

            this.InvalidateVisual();
        }

        private void Display_MouseMove(object sender, MouseEventArgs e)
        {
            PrevMovedMouseTilePos = MovedMouseTilePos;
            MovedMouseTilePos = this.logic.GetTilePos(this.PointToScreen(Mouse.GetPosition(this)));
            if (PrevMovedMouseTilePos != MovedMouseTilePos)
            {
                this.InvalidateVisual();
            }
        }

        private void SetTimers() {
            this.EnemySpawnTimer = new DispatcherTimer();
            this.EnemySpawnTimer.Interval = TimeSpan.FromMilliseconds(4000);
            this.EnemySpawnTimer.Tick += EnemySpawnTimer_Tick;

            DispatcherTimer dt = new DispatcherTimer();

            dt.Interval = TimeSpan.FromMilliseconds(30);
            dt.Tick += (sender, eventargs) => {
                foreach (var item in this.model.SpawnedEnemies)
                {
                    item.Move();
                }
                InvalidateVisual();
            };

            this.EnemySpawnTimer.Start();
            dt.Start();

        }

        private void EnemySpawnTimer_Tick(object? sender, EventArgs e)
        {
            this.logic.EnemySpawnTime();
            this.InvalidateVisual();
        }


        private void Display_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(this);
            Point tilePos = this.logic.GetTilePos(mousePos);
            bool nowCLicked = false;
            //mouseMoved = false;

            //which button clicked if clicked
            if (tilePos.X == this.model.Map[0].Length)
            {
                nowCLicked = true;
                if (tilePos.Y == 0)
                {
                    if (this.model.DeployKnight)
                    {
                        this.model.DeployKnight = false;
                    }
                    else
                    {
                        this.model.DeployKnight = true;
                        this.model.RemoveUnit = false;
                        this.model.MoveUnit = false;
                    }
                }
            }
            else if (tilePos.X == this.model.Map[0].Length - 2 && tilePos.Y == 5)
            {
                nowCLicked = true;
                if (this.model.MoveUnit)
                {
                    this.model.MoveUnit = false;
                }
                else
                {
                    this.model.MoveUnit = true;
                    this.model.DeployKnight = false;
                    this.model.RemoveUnit = false;
                    this.model.UpgradeUnit = false;
                }
            }
            else if (tilePos.X == this.model.Map[0].Length - 1 && tilePos.Y == 5)
            {
                nowCLicked = true;
                if (this.model.RemoveUnit)
                {
                    this.model.RemoveUnit = false;
                }
                else
                {
                    this.model.RemoveUnit = true;
                    this.model.DeployKnight = false;
                    this.model.MoveUnit = false;
                    this.model.UpgradeUnit = false;

                }
            }
            else if (tilePos.X == this.model.Map[0].Length - 3 && tilePos.Y == 5)
            {
                nowCLicked = true;
                if (this.model.UpgradeUnit)
                {
                    this.model.UpgradeUnit = false;
                }
                else
                {
                    this.model.UpgradeUnit = true;
                    this.model.DeployKnight = false;
                    this.model.MoveUnit = false;
                    this.model.RemoveUnit = false;

                }
            }


            //what will happan if Knight button clicked and clicked another cell.
            if (!nowCLicked && this.model.DeployKnight && (int)tilePos.X < Config.ColumnNumbers-1)
            {
                this.logic.DeployKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.DeployKnight = false;
            }
            else if (!nowCLicked && this.model.RemoveUnit)
            {
                this.logic.RemoveKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.RemoveUnit = false;
            }
            else if (!nowCLicked && this.model.UpgradeUnit)
            {
                this.logic.UpgradeKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.UpgradeUnit = false;
            }
            else if (!nowCLicked && this.model.MoveUnit)
            {
                if (MovedUnitPrevPos.X != -1){
                    this.logic.MoveKnight((int)tilePos.X, (int)tilePos.Y, (int)MovedUnitPrevPos.X, (int)MovedUnitPrevPos.Y);

                    this.model.MoveUnit = false;
                    MovedUnitPrevPos = new Point(-1, -1);
                }
                else{ // == -1
                    MovedUnitPrevPos = this.logic.GetTilePos(mousePos);
                }
            }


            this.InvalidateVisual();
        }
    }
}
