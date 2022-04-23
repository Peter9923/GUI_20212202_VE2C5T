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
                this.Model.Gold += (int)((k.Level * k.Cost) / 1.25);
                this.Model.Map[y][x] = null;
            }
        }

        public void UpgradeKnight(int x, int y)
        {
            if (this.Model.Map[y][x] != null && this.Model.Map[y][x] is IAllied actual
                && this.Model.Gold >= actual.UpgradeCost ) {
                this.Model.Gold -= actual.UpgradeCost;
                this.Model.Map[y][x].Level++;
                this.Model.Map[y][x].MaxLife *= 1.5;
                this.Model.Map[y][x].ActualLife = this.Model.Map[y][x].MaxLife;
            }
        }

        public void MoveKnight(int actualX, int actualY, int prevX, int prevY)
        {
            if (this.Model.Map[actualY][actualX] == null
                && this.Model.Map[prevY][prevX] != null && this.Model.Map[prevY][prevX] is IAllied actual
                && this.Model.Gold >= (actual.UpgradeCost / 2)){

                this.Model.Gold -= actual.UpgradeCost / 2;

                this.Model.Map[actualY][actualX] = this.Model.Map[prevY][prevX];
                this.Model.Map[prevY][prevX] = null;

            }
        }
    }
}
