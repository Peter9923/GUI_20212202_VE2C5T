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

        public void DeployKnight(int x, int y)
        {
            if (this.Model.Map[y][x] == null){
                if (this.Model.DeployKnight && this.Model.Gold >= Config.KnightCost)
                {
                    this.Model.Map[y][x] = new Knight(x, y);
                    this.Model.Gold -= (this.Model.Map[y][x] as Knight).Cost;
                }
            }
            
        }
        public void RemoveKnight(int x, int y)
        {
            if (this.Model.Map[y][x] is Knight k){
                this.Model.Gold += (k.Level * k.Cost) / 2;
                this.Model.Map[y][x] = null;
            }
        }

    }
}
