using GameLogic.Interfaces;
using GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLogic.Classes
{
    public class DisplayLogic : IDisplayLogic{
        public DisplayLogic(){

        }

        public Point GetTilePos(Point mousePos)
        {
            return new Point(
                (int)((mousePos.X / Config.TileSize) - 1),
                (int)(mousePos.Y / Config.TileSize));
        }
    }
}
