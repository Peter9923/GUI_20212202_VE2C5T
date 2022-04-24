﻿using GameLogic;
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
        private List<Brush> ArcherBrushes;
        private Brush ArrowBrush;

        //Button Brushes
        private Brush DeployKnightBrush;
        private Brush DeployKnightSelectedBrush;
        private Brush DeployArcherBrush;
        private Brush DeployArcherSelectedBrush;

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
        private DispatcherTimer EnemyMoveTimer;
        private DispatcherTimer AttackTimer;

        private DispatcherTimer ArcherAnimationTimer;
        private DispatcherTimer ArcherArrowMoveTimer;

        private DispatcherTimer MouseMovingTimer;

        Point MovedMouseTilePos;
        Point PrevMovedMouseTilePos;


        Point MovedUnitPrevPos;

       


        public Display(IModel model, ILogic logic)
        {
            this.model = model;
            this.logic = logic;

            KnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Knight.png", UriKind.RelativeOrAbsolute)));
            EnemyKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\EnemyKnight.png", UriKind.RelativeOrAbsolute)));
            ArcherBrushes = new List<Brush>();
            for (int i = 0; i < 8; i++){
                ArcherBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\archer{i}.png", UriKind.RelativeOrAbsolute))));
            }
            ArrowBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Arrow.png", UriKind.RelativeOrAbsolute)));


            DeployKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnight.png", UriKind.RelativeOrAbsolute)));
            DeployKnightSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnightSelected.png", UriKind.RelativeOrAbsolute)));

            DeployArcherBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployArcher.png", UriKind.RelativeOrAbsolute)));
            DeployArcherSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployArcherSelected.png", UriKind.RelativeOrAbsolute)));


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

                this.SetTimers();
            }

            this.InvalidateVisual();
        }

       

        private void SetTimers()
        {
            this.EnemySpawnTimer = new DispatcherTimer();
            this.EnemySpawnTimer.Interval = TimeSpan.FromMilliseconds(4000);
            this.EnemySpawnTimer.Tick += EnemySpawnTimer_Tick;

            this.EnemyMoveTimer = new DispatcherTimer();
            this.EnemyMoveTimer.Interval = TimeSpan.FromMilliseconds(75);
            this.EnemyMoveTimer.Tick += EnemyMoveTimer_Tick;

            this.ArcherAnimationTimer = new DispatcherTimer();
            this.ArcherAnimationTimer.Interval = TimeSpan.FromMilliseconds(250);
            this.ArcherAnimationTimer.Tick += ArcherAnimationTimer_Tick;

            this.MouseMovingTimer = new DispatcherTimer();
            this.MouseMovingTimer.Interval = TimeSpan.FromMilliseconds(0.25);
            this.MouseMovingTimer.Tick += MouseMovingTimer_Tick;


            this.AttackTimer = new DispatcherTimer();
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(250);
            this.AttackTimer.Tick += AttackTimer_Tick;

            this.ArcherArrowMoveTimer = new DispatcherTimer();
            this.ArcherArrowMoveTimer.Interval = TimeSpan.FromMilliseconds(25);
            this.ArcherArrowMoveTimer.Tick += ArcherArrowMoveTimer_Tick;

            

            this.MouseMovingTimer.Start();
            this.EnemySpawnTimer.Start();
            this.EnemyMoveTimer.Start();
            this.ArcherAnimationTimer.Start();
            this.AttackTimer.Start();
            this.ArcherArrowMoveTimer.Start();
        }

        private void ArcherArrowMoveTimer_Tick(object? sender, EventArgs e){
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is Archer archer)
                    {
                        for (int i = 0; i < archer.Arrows.Count; i++)
                        {
                            archer.Arrows[i].Move();
                        }

                    }
                }
            }
        }

        private void AttackTimer_Tick(object? sender, EventArgs e)
        {
            List<EnemyKnight> shouldDelete = new List<EnemyKnight>();
            foreach (var enemy in this.model.SpawnedEnemies)
            {
                for (int y = 0; y < this.model.Map.Length; y++)
                {
                    for (int x = 0; x < this.model.Map[y].Length; x++)
                    {
                        if (this.model.Map[y][x] is IAllied allied)
                        {

                            if (enemy.IsCollision(allied))
                            {
                                this.logic.EnemyAndAlliedUnitMetEachOther(enemy, allied);
                                if (enemy.ShouldDie)
                                {
                                    shouldDelete.Add(enemy);
                                }
                            }

                        }
                    }
                }
            }

            foreach (var diedEnemy in shouldDelete)
            {
                this.model.SpawnedEnemies.Remove(diedEnemy);
            }
            shouldDelete.Clear();

            InvalidateVisual();
        }

        private void MouseMovingTimer_Tick(object? sender, EventArgs e)
        {
            PrevMovedMouseTilePos = MovedMouseTilePos;
            MovedMouseTilePos = this.logic.GetTilePos(this.PointToScreen(Mouse.GetPosition(this)));
            if (PrevMovedMouseTilePos != MovedMouseTilePos){
                this.InvalidateVisual();
            }
        }

        private void ArcherAnimationTimer_Tick(object? sender, EventArgs e)
        {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is Knight knight)
                    {
                        //To do
                    }
                    else if (this.model.Map[y][x] is Archer archer)
                    {
                        if (archer.AnimationIndex == this.ArcherBrushes.Count - 1)
                        {
                            archer.AnimationIndex = 0;
                            this.logic.GenerateArrow(x,y);
                        }
                        else
                        {
                            archer.AnimationIndex++;
                        }
                    }
                }
            }
        }

        private void EnemyMoveTimer_Tick(object? sender, EventArgs e)
        {
            List<EnemyKnight> shouldDelete = new List<EnemyKnight>();
            foreach (var enemy in this.model.SpawnedEnemies)
            {
                bool wasCollision = false;
                for (int y = 0; y < this.model.Map.Length; y++)
                {
                    for (int x = 0; x < this.model.Map[y].Length; x++)
                    {
                        if (this.model.Map[y][x] is IAllied allied)
                        {
                            if (enemy.IsCollision(allied))
                            {
                                wasCollision = true;
                            }

                        }
                    }
                }
                if (wasCollision == false)
                {
                    enemy.Move();

                    if (enemy.Position.X <= Config.TileSize/2){
                        this.logic.EnemyIsInTheCastle(enemy);
                        if (enemy.ShouldDie){
                            shouldDelete.Add(enemy);
                        }
                    }
                }
            }

            foreach (var diedEnemy in shouldDelete)
            {
                this.model.SpawnedEnemies.Remove(diedEnemy);
            }
            shouldDelete.Clear();

            InvalidateVisual();
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
                        this.model.UpgradeUnit = false;
                        this.model.DeployArcher = false;
                    }
                }
                if (tilePos.Y == 1)
                {
                    if (this.model.DeployArcher)
                    {
                        this.model.DeployArcher = false;
                    }
                    else
                    {
                        this.model.DeployArcher = true;
                        this.model.RemoveUnit = false;
                        this.model.MoveUnit = false;
                        this.model.UpgradeUnit = false;
                        this.model.DeployKnight = false;
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
                    this.model.DeployArcher = false;
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
                    this.model.DeployArcher = false;

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
                    this.model.DeployArcher = false;

                }
            }


            //what will happan if Knight button clicked and clicked another cell.
            if (!nowCLicked && this.model.DeployKnight)
            {
                this.logic.DeployKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.DeployKnight = false;
            }
            else if (!nowCLicked && this.model.DeployArcher)
            {
                this.logic.DeployKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.DeployArcher = false;
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
                if (MovedUnitPrevPos.X != -1)
                {
                    this.logic.MoveKnight((int)tilePos.X, (int)tilePos.Y, (int)MovedUnitPrevPos.X, (int)MovedUnitPrevPos.Y);

                    this.model.MoveUnit = false;
                    MovedUnitPrevPos = new Point(-1, -1);
                }
                else
                { // == -1
                    MovedUnitPrevPos = this.logic.GetTilePos(mousePos);
                }
            }


            this.InvalidateVisual();
        }




        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (drawingContext != null){
                if (this.model != null){
                    if (backgroundRendererCount == 0){
                        DrawBackgroundElements(drawingContext);
                    }

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
                            else if (this.model.DeployArcher)
                            {
                                drawingContext.DrawGeometry(this.ArcherBrushes[0], new Pen(Brushes.Black, 8), new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
                            }
                            else if (this.model.MoveUnit)
                            {
                                DrawBorder(drawingContext);
                            }
                        }
                        DrawBorder(drawingContext);
                    }


                    DrawButtons(drawingContext);
                    DrawKnights(drawingContext);
                    DrawEnemies(drawingContext);
                    DrawForegroundElements(drawingContext);

                    DrawArrows(drawingContext);
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
            DeployArcherButton(drawingContext);


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
        private void DeployArcherButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(
                   (this.model.Map[0].Length + 1) * Config.TileSize, 1 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.DeployArcher)
            {
                drawingContext.DrawGeometry(this.DeployArcherSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.DeployArcherBrush, null, g);
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
            DrawAliedLevelsAndHps(drawingContext);

            DrawEnemyHpsAndLevels(drawingContext);
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

        private void DrawAliedLevelsAndHps(DrawingContext drawingContext)
        {
            FormattedText text;
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied actual)
                    {
                        Geometry rect1 = new RectangleGeometry(new Rect((x + 1) * Config.TileSize + 50, y * Config.TileSize, 80, 15));
                        drawingContext.DrawGeometry(Brushes.WhiteSmoke, null, rect1);

                        Geometry rect2 = new RectangleGeometry(new Rect((x + 1) * Config.TileSize + 50, y * Config.TileSize, ((80 * actual.ActualLife) / actual.MaxLife), 15));
                        drawingContext.DrawGeometry(Brushes.DarkRed, null, rect2);

                        text = new FormattedText(this.model.Map[y][x].Level.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 45, Brushes.Black, 1);
                        drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.WhiteSmoke, 1), text.BuildGeometry(new Point((x + 1) * Config.TileSize, y * Config.TileSize)));
                    }
                }
            }
        }

        private void DrawEnemyHpsAndLevels(DrawingContext drawingContext)
        {
            FormattedText text;
            foreach (var enemy in this.model.SpawnedEnemies)
            {
                Geometry rect1 = new RectangleGeometry(new Rect(enemy.Position.X + Config.TileSize/2, enemy.Position.Y, 80, 15));
                drawingContext.DrawGeometry(Brushes.WhiteSmoke, null, rect1);

                Geometry rect2 = new RectangleGeometry(new Rect( (enemy.Position.X + Config.TileSize / 2) + 80 - (((80 * enemy.ActualLife) / enemy.MaxLife)), enemy.Position.Y, ((80 * enemy.ActualLife) / enemy.MaxLife ), 15));
                drawingContext.DrawGeometry(Brushes.DarkRed, null, rect2);

                text = new FormattedText(enemy.Level.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 45, Brushes.WhiteSmoke, 3);
                drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.WhiteSmoke, 1), text.BuildGeometry(new Point(enemy.Position.X+20, enemy.Position.Y)));
            }
        }
        #endregion



        private void DrawKnights(DrawingContext drawingContext) {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is Knight knight)
                    {
                        drawingContext.DrawGeometry(this.KnightBrush, null, knight.RealArea);
                    }
                    else if (this.model.Map[y][x] is Archer archer)
                    {
                        drawingContext.DrawGeometry(this.ArcherBrushes[archer.AnimationIndex], null, archer.RealArea);
                    }
                }
            }
        }
        private void DrawEnemies(DrawingContext drawingContext)
        {
            foreach (var enemy in this.model.SpawnedEnemies)
            {
                drawingContext.DrawGeometry(this.EnemyKnightBrush, null, enemy.RealArea);
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

        private void DrawArrows(DrawingContext drawingContext) {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is Archer archer){

                        for (int i = 0; i < archer.Arrows.Count; i++) {
                            drawingContext.DrawGeometry(this.ArrowBrush, null, archer.Arrows[i].RealArea);
                        }

                    }
                }
            }
        }

    }
}
