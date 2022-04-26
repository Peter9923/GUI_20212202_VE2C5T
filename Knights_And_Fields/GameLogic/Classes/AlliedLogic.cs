using GameLogic.Interfaces;
using GameModel;
using GameModel.Interfaces;
using GameModel.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Classes
{
    public class AlliedLogic : IAlliedLogic
    {
        private static Random rnd;
        IModel model;

        public AlliedLogic(IModel model)
        {
            this.model = model;
            rnd = new Random();
        }

        

        public void GenerateArrow(int x, int y)
        {
            if (this.model.Map[y][x] != null &&
                this.model.Map[y][x] is Archer archer)
            {
                Bullet arrow = new Bullet(((x + 1) * Config.TileSize), (y * Config.TileSize), 10);
                archer.AddArrow(arrow);
            }
        }

        public void CheckWhichArrowShouldDraw(IUnit allied)
        {
            if (allied is Archer archer){
                for (int i = 0; i < archer.Arrows.Count; i++)
                {
                    if (archer.Arrows[i].Position.X >= (Config.TileSize * Config.ColumnNumbers)) {
                        archer.Arrows[i] = null;
                    }

                    for (int k = 0; k < this.model.SpawnedEnemies.Count; k++){

                        if(archer.Arrows[i] != null && archer.Arrows[i].IsCollision(this.model.SpawnedEnemies[k])){
                            
                            this.model.SpawnedEnemies[k].ActualLife -= archer.Damage;
                            archer.Arrows[i] = null;

                            if (this.model.SpawnedEnemies[k].ActualLife <= 0){

                                if (this.model.SpawnedEnemies[k] is EnemyGhost){
                                    this.model.DiedItems.Add( new DyingItems(this.model.SpawnedEnemies[k].Position.X, this.model.SpawnedEnemies[k].Position.Y, UnitsWhatCanDie.Ghost) );
                                }
                                else if (this.model.SpawnedEnemies[k] is EnemyGhost2)
                                {
                                    this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[k].Position.X, this.model.SpawnedEnemies[k].Position.Y, UnitsWhatCanDie.Ghost2));
                                }

                                this.model.Score += this.model.SpawnedEnemies[k].Level * 5;
                                this.model.SpawnedEnemies[k] = null;
                            }

                        }

                    }
                }

                archer.Arrows = archer.Arrows.Where(x => x != null).ToList();
            }
        }

        public void BulletsMoving() {
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

    }
}
