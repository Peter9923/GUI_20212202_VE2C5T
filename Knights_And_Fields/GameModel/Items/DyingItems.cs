using GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel.Items
{
    public class DyingItems : IDyingItems
    {
        public DyingItems(double X, double Y, UnitsWhatCanDie unitName)
        {
            Position = new Point(X, Y);
            WhoDied = unitName;
        }
        public UnitsWhatCanDie WhoDied { get; set; }
        public Point Position { get; set; }
        public Geometry RealArea
        {
            get
            {
                return new RectangleGeometry(new Rect(Position.X, Position.Y, Config.TileSize, Config.TileSize));
            }
        }
        public int DieIndex { get; set; }
    }
}
