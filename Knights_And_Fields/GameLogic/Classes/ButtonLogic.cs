using GameLogic.Interfaces;
using GameModel;
using GameModel.Interfaces;
using GameModel.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLogic.Classes
{
    public class ButtonLogic : IButtonLogic{

        IModel Model { get; set; }

        private const int KnightCost = 150;
        private const int ArcherCost = 250;
        private const int WallCost = 100;

        private const int ScorePointForDeployUnit = 5;

        public ButtonLogic(IModel model){
            this.Model = model;
        }

        public void DeployUnit(int x, int y)
        {
            if (x < Config.ColumnNumbers && y < Config.RowNumbers){

                if (this.Model.Map[y][x] == null
                && x < Config.ColumnNumbers - 1)
                {
                    if (this.Model.DeployKnight && this.Model.Gold >= KnightCost)
                    {
                        this.Model.Map[y][x] = new Knight(x, y);
                        this.Model.Gold -= KnightCost;
                        this.Model.Score += 5;
                    }
                    else if (this.Model.DeployArcher && this.Model.Gold >= ArcherCost)
                    {
                        this.Model.Map[y][x] = new Archer(x, y);
                        this.Model.Gold -= ArcherCost;
                        this.Model.Score += 5;
                    }
                    else if (this.Model.DeployWall && this.Model.Gold >= WallCost)
                    {
                        this.Model.Map[y][x] = new Wall(x, y);
                        this.Model.Gold -= WallCost;
                        this.Model.Score += 5;
                    }
                }

            }

            
        }
        public void RemoveUnit(int x, int y)
        {
            if (x < Config.ColumnNumbers && y < Config.RowNumbers)
            {
                if (this.Model.Map[y][x] != null && this.Model.Map[y][x] is IAllied alliedUnit)
                {
                    this.Model.Gold += (int)((double)alliedUnit.UpgradeCost / (double)2);
                    this.Model.Map[y][x] = null;
                }
            }
        }

        public void UpgradeUnit(int x, int y)
        {
            if (x < Config.ColumnNumbers && y < Config.RowNumbers)
            {
                if (this.Model.Map[y][x] != null && this.Model.Map[y][x] is IAllied alliedUnit
                && this.Model.Gold >= alliedUnit.UpgradeCost)
                {
                    this.Model.Gold -= alliedUnit.UpgradeCost;
                    this.Model.Map[y][x].Level++;
                }
            }
        }

        public void MoveUnit(int actualX, int actualY, int prevX, int prevY){

            if (actualX < Config.ColumnNumbers && actualY < Config.RowNumbers &&
                prevX < Config.ColumnNumbers && prevY < Config.RowNumbers)
            {
                if (this.Model.Map[actualY][actualX] == null
                && this.Model.Map[prevY][prevX] != null && this.Model.Map[prevY][prevX] is IAllied alliedUnit
                && this.Model.Gold >= (alliedUnit.UpgradeCost / 2))
                {

                    this.Model.Gold -= alliedUnit.UpgradeCost / 2;

                    this.Model.Map[actualY][actualX] = this.Model.Map[prevY][prevX];
                    this.Model.Map[actualY][actualX].Position = new Point(actualX, actualY);
                    this.Model.Map[prevY][prevX] = null;
                }
            }

            
        }


    }
}
