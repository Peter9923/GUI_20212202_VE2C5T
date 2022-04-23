using GameModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameRenderer
{
    public class Renderer
    {
        private IModel model;
        private Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();

        public Renderer(IModel model)
        {
            this.model = model;
        }

        //Brushes
        private Brush GrassBrush
        {
            get { return this.GetBrush("GameRenderer.Images.grass.png", true); }
        }
        private Brush EnemyGrassBrush
        {
            get { return this.GetBrush("GameRenderer.Images.EnemyGrass.png", true); }
        }

        private Brush CastleWallBrush
        {
            get { return this.GetBrush("GameRenderer.Images.CastleWall.png", true); }
        }

        private Brush KnightBrush
        {
            get { return this.GetBrush("GameRenderer.Images.Knight.png", true); }
        }



        //Button Brushes
        private Brush DeployKnightBrush
        {
            get { return this.GetBrush("GameRenderer.Images.DeployKnight.png", false); }
        }

        private Brush DeployKnightSelectedBrush
        {
            get { return this.GetBrush("GameRenderer.Images.DeployKnightSelected.png", false); }
        }


        private Brush MoveButtonBrush
        {
            get { return this.GetBrush("GameRenderer.Images.Move.png", false); }
        }

        private Brush MoveButtonSelectedBrush
        {
            get { return this.GetBrush("GameRenderer.Images.MoveSelected.png", false); }
        }

        private Brush RemoveButtonBrush
        {
            get { return this.GetBrush("GameRenderer.Images.Remove.png", false); }
        }

        private Brush RemoveButtonSelectedBrush
        {
            get { return this.GetBrush("GameRenderer.Images.RemoveSelected.png", false); }
        }

        private Brush UpgradeButtonBrush
        {
            get { return this.GetBrush("GameRenderer.Images.Upgrade.png", false); }
        }

        private Brush UpgradeButtonSelectedBrush
        {
            get { return this.GetBrush("GameRenderer.Images.UpgradeSelected.png", false); }
        }



        //Drawings
        private Drawing Background;
        private Drawing EnemyBackground;
        private Drawing CastleHPText;
        private Drawing GoldText;
        private Drawing CastleWall;


        //BuildDrawing methods

        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetBackground());
            dg.Children.Add(this.GetEnemyBackground());

            dg.Children.Add(this.GetCastleHpBoxBackground());
            dg.Children.Add(this.GetCastleHpBoxForeground());
            dg.Children.Add(this.GetCastleHpText());
            dg.Children.Add(this.GetCurrentCastleHp());
            dg.Children.Add(this.GetGoldText());
            dg.Children.Add(this.GetCurrentGold());
            dg.Children.Add(this.GetWave());
            dg.Children.Add(this.GetScore());


            dg.Children.Add(this.GetKnightButton());
            dg.Children.Add(this.GetMoveButton());
            dg.Children.Add(this.GetRemoveButton());
            dg.Children.Add(this.GetUpgradeButton());


            dg.Children.Add(this.GetCastleWall());
            dg.Children.Add(this.GetKnights());

            dg.Children.Add(this.GetLevels());
            dg.Children.Add(this.GetHpsBackground());
            dg.Children.Add(this.GetHpsForeground());

            return dg;
        }

        //Get - Methods
        private Brush GetBrush(string fname, bool isTiled){
            if (!this.brushes.ContainsKey(fname)){
                BitmapImage png = new BitmapImage();
                png.BeginInit();
                png.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);
                png.EndInit();
                ImageBrush ib = new ImageBrush(png);
                if (isTiled){
                    ib.TileMode = TileMode.Tile;
                    ib.Viewport = new Rect(0, 0, Config.TileSize, Config.TileSize);
                    ib.ViewportUnits = BrushMappingMode.Absolute;
                }

                this.brushes.Add(fname, ib);
            }

            return this.brushes[fname];
        }

        private Drawing GetBackground(){
            if (Background == null){
                GeometryGroup g = new GeometryGroup();
                for (int y = 0; y < this.model.Map.Length; y++)
                {
                    for (int x = 0; x < this.model.Map[y].Length-1; x++)
                    {
                        Geometry box = new RectangleGeometry(new Rect((x + 1) * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                        g.Children.Add(box);
                    }
                }
                Background = new GeometryDrawing(this.GrassBrush, null, g);
            }

            return Background;
        }
        private Drawing GetEnemyBackground()
        {
            if (EnemyBackground == null)
            {
                GeometryGroup g = new GeometryGroup();
                for (int y = 0; y < this.model.Map.Length; y++)
                {
                    Geometry box = new RectangleGeometry(new Rect(10 * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                    g.Children.Add(box);
                }
                EnemyBackground = new GeometryDrawing(this.EnemyGrassBrush, null, g);
            }

            return EnemyBackground;
        }

        private Drawing GetCastleWall()
        {
            if (CastleWall == null){
                GeometryGroup g = new GeometryGroup();
                for (int y = 0; y < this.model.Map.Length; y++)
                {
                    Geometry box = new RectangleGeometry(new Rect(0 * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                    g.Children.Add(box);
                }

                CastleWall = new GeometryDrawing(this.CastleWallBrush, null, g);
            }

            return CastleWall;
        }


        #region FormattedTexts - and Box backgrounds and foregrounds
        private Drawing GetCastleHpText()
        {
            if (CastleHPText == null){
                FormattedText text = new FormattedText("Castle HP: ", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
                CastleHPText =  new GeometryDrawing(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(20, 825)));
            }
            return CastleHPText;
        }
        private Drawing GetCastleHpBoxBackground() {
            Geometry rect1 = new RectangleGeometry(new Rect(180, 825, 300, 30));
            return new GeometryDrawing(Brushes.WhiteSmoke, null, rect1);            
        }
        private Drawing GetCastleHpBoxForeground()
        {
            Geometry rect2 = new RectangleGeometry(new Rect(180, 825, ( (300 * this.model.CastleActualHP) / this.model.CastleMaxHP), 30));
            return new GeometryDrawing(Brushes.DarkRed, null, rect2);
        }
        private Drawing GetCurrentCastleHp()
        {
            FormattedText text = new FormattedText(this.model.CastleActualHP.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            return new GeometryDrawing(null, new Pen((this.model.CastleActualHP >= 150 ? Brushes.DarkGreen : Brushes.DarkRed), 2), text.BuildGeometry(new Point(310, 825)));
        }
        private Drawing GetGoldText(){
            if (GoldText == null){
                FormattedText text1 = new FormattedText("Gold: ", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 2);
                GoldText = new GeometryDrawing(null, new Pen(Brushes.Black, 2), text1.BuildGeometry(new Point(500, 825)));
            }
            return GoldText;
        }
        private Drawing GetCurrentGold(){
            FormattedText text1 = new FormattedText(this.model.Gold.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Yellow, 2);
            return new GeometryDrawing(null, new Pen(Brushes.Yellow, 2), text1.BuildGeometry(new Point(580, 825)));
        }
        private Drawing GetWave()
        {
            FormattedText text = new FormattedText("Wave: " + this.model.Wave.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            return new GeometryDrawing(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(20, 900)));
        }

        private Drawing GetScore()
        {
            FormattedText text = new FormattedText("Score: " + this.model.Score.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            return new GeometryDrawing(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(500, 900)));
        }

        private Drawing GetLevels()
        {
            GeometryGroup g = new GeometryGroup();
            FormattedText text;
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied)
                    {
                        text = new FormattedText(this.model.Map[y][x].Level.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 45, Brushes.Black, 1);
                        g.Children.Add(text.BuildGeometry(new Point((x + 1) * Config.TileSize, y * Config.TileSize)));
                    }
                    
                }
            }

            return new GeometryDrawing(Brushes.WhiteSmoke, null, g);
        }

        private Drawing GetHpsBackground()
        {
            GeometryGroup g = new GeometryGroup();
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied)
                    {
                        Geometry rect1 = new RectangleGeometry(new Rect((x+1) * Config.TileSize + 50, y * Config.TileSize, 80, 15));
                        g.Children.Add(rect1);
                    }

                }
            }

            return new GeometryDrawing(Brushes.WhiteSmoke, null, g);
        }

        private Drawing GetHpsForeground()
        {
            GeometryGroup g = new GeometryGroup();
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied actual)
                    {
                        Geometry rect1 = new RectangleGeometry(new Rect((x + 1) * Config.TileSize + 50, y * Config.TileSize, ( (80 * actual.ActualLife) / actual.MaxLife), 15));
                        g.Children.Add(rect1);
                    }

                }
            }

            return new GeometryDrawing(Brushes.DarkRed, null, g);
        }

        #endregion

        #region Buttons
        private Drawing GetKnightButton()
        {
            Geometry g = new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length + 1) * Config.TileSize, 0 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.DeployKnight)
            {
                return new GeometryDrawing(this.DeployKnightSelectedBrush, null, g);
            }

            return new GeometryDrawing(this.DeployKnightBrush, null, g);
        }

        private Drawing GetMoveButton()
        {
            Geometry g = new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length - 1) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.MoveUnit)
            {
                return new GeometryDrawing(this.MoveButtonSelectedBrush, null, g);
            }

            return new GeometryDrawing(this.MoveButtonBrush, null, g);
        }

        private Drawing GetRemoveButton()
        {
            Geometry g = new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.RemoveUnit)
            {
                return new GeometryDrawing(this.RemoveButtonSelectedBrush, null, g);
            }

            return new GeometryDrawing(this.RemoveButtonBrush, null, g);
        }

        private Drawing GetUpgradeButton()
        {
            Geometry g = new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length - 2) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize));

            if (this.model.UpgradeUnit)
            {
                return new GeometryDrawing(this.UpgradeButtonSelectedBrush, null, g);
            }

            return new GeometryDrawing(this.UpgradeButtonBrush, null, g);
        }
        #endregion


        //Allieds
        private Drawing GetKnights()
        {
            GeometryGroup g = new GeometryGroup();
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is Knight)
                    {
                        Geometry box = new RectangleGeometry(new Rect((x + 1) * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                        g.Children.Add(box);
                    }
                }
            }

            return new GeometryDrawing(this.KnightBrush, null, g);
        }
        public Drawing GetKnightIfMouseMove(double x, double y)
        {
            Geometry box = new RectangleGeometry(new Rect((x + 1) * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
            GeometryDrawing gd = new GeometryDrawing(this.KnightBrush, new Pen(Brushes.Black, 3), box);
            return gd;
        }
        public Drawing GetBorderIfMouseMove(double x, double y) {
            Geometry box = new RectangleGeometry(new Rect((x + 1) * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
            GeometryDrawing gd = null;
            if (this.model.RemoveUnit){
                gd = new GeometryDrawing(null, new Pen(Brushes.Red, 6), box);
            }
            else if (this.model.MoveUnit){
                gd = new GeometryDrawing(null, new Pen(Brushes.Aqua, 6), box);
            }
            else if (this.model.UpgradeUnit){
                gd = new GeometryDrawing(null, new Pen(Brushes.Purple, 6), box);
            }
            else{
                gd = new GeometryDrawing(null, new Pen(Brushes.Yellow, 6), box);
            }

           
            return gd;
        }


    }
}
