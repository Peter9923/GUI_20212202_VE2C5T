using GameModel;
using System;
using System.Collections.Generic;
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

        //Drawings
        private Drawing Background;


        //BuildDrawing methods

        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetBackground());

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
                    for (int x = 0; x < this.model.Map[y].Length; x++)
                    {
                        Geometry box = new RectangleGeometry(new Rect((x + 1) * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                        g.Children.Add(box);
                    }
                }
                Background = new GeometryDrawing(this.GrassBrush, null, g);
            }

            return Background;
        }



    }
}
