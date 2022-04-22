using GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLogic
{
    public class Logic : ILogic
    {
        public IModel Model { get; set; }
        public Logic(IModel model)
        {
            this.Model = model;
        }

        // Pixel position --> Tile position
        public Point GetTilePos(Point mousePos)
        {
            return new Point(
                (int)((mousePos.X / Config.TileSize) - 1),
                (int)(mousePos.Y / Config.TileSize));
        }

    }
}
