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
                                else if (this.model.SpawnedEnemies[k] is EnemyGhost3)
                                {
                                    this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[k].Position.X, this.model.SpawnedEnemies[k].Position.Y, UnitsWhatCanDie.Ghost3));
                                }
                                else if (this.model.SpawnedEnemies[k] is EnemyOrc1)
                                {
                                    this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[k].Position.X, this.model.SpawnedEnemies[k].Position.Y, UnitsWhatCanDie.Orc1));
                                }
                                else if (this.model.SpawnedEnemies[k] is EnemyOrc2)
                                {
                                    this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[k].Position.X, this.model.SpawnedEnemies[k].Position.Y, UnitsWhatCanDie.Orc2));
                                }
                                else if (this.model.SpawnedEnemies[k] is EnemyOrc3)
                                {
                                    this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[k].Position.X, this.model.SpawnedEnemies[k].Position.Y, UnitsWhatCanDie.Orc3));
                                }

                                this.model.Score += this.model.SpawnedEnemies[k].Level * 5;

                                if (this.model.SpawnedEnemies[k] is EnemyGhost || this.model.SpawnedEnemies[k] is EnemyGhost2 || this.model.SpawnedEnemies[k] is EnemyGhost3)
                                {
                                    this.model.SOUNDS.GhostDied.Open(new Uri("Sounds\\ghostDied.mp3", UriKind.RelativeOrAbsolute));
                                    this.model.SOUNDS.GhostDied.Play();
                                }

                                this.model.Score += (this.model.SpawnedEnemies[k].Level * 3);
                                this.model.Gold += (this.model.SpawnedEnemies[k].Level * 5);
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

        public void CheckWhichAlliedShouldAttack() {


            for (int i = 0; i < this.model.Map.Length; i++)
            {
                for (int j = 0; j < this.model.Map[i].Length; j++){


                    if (this.model.Map[i][j] != null || this.model.Map[i][j] is Knight){

                        foreach (var enemy in this.model.SpawnedEnemies)
                        {

                            if (enemy != null && (this.model.Map[i][j].IsCollision(enemy) == true || enemy.IsCollision(this.model.Map[i][j]))){
                                this.model.Map[i][j].ShouldAttack = true;
                                this.model.Map[i][j].ShouldWalk = false;
                                break;
                            }
                            else{
                                this.model.Map[i][j].ShouldAttack = false;
                                this.model.Map[i][j].ShouldWalk = true;
                            }
                        }
                    }
                }
            }


        }

        public void AlliedAttackAnEnemy(IUnit allied) {
            for (int i = 0; i < this.model.SpawnedEnemies.Count; i++)
            {
                if (allied != null && this.model.SpawnedEnemies[i] != null &&
                        (this.model.SpawnedEnemies[i].IsCollision(allied) == true ||allied.IsCollision(this.model.SpawnedEnemies[i]) == true))
                {
                    this.model.SpawnedEnemies[i].ActualLife -= allied.Damage;

                    if (this.model.SpawnedEnemies[i].ActualLife <= 0)
                    {
                        if (this.model.SpawnedEnemies[i] is EnemyGhost || this.model.SpawnedEnemies[i] is EnemyGhost2)
                        {
                            this.model.SOUNDS.GhostDied.Open(new Uri("Sounds\\ghostDied.mp3", UriKind.RelativeOrAbsolute));
                            this.model.SOUNDS.GhostDied.Play();
                        }

                        if (this.model.SpawnedEnemies[i] is EnemyGhost)
                        {
                            this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[i].Position.X, this.model.SpawnedEnemies[i].Position.Y, UnitsWhatCanDie.Ghost));
                        }
                        else if (this.model.SpawnedEnemies[i] is EnemyGhost2)
                        {
                            this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[i].Position.X, this.model.SpawnedEnemies[i].Position.Y, UnitsWhatCanDie.Ghost2));
                        }
                        else if (this.model.SpawnedEnemies[i] is EnemyGhost3)
                        {
                            this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[i].Position.X, this.model.SpawnedEnemies[i].Position.Y, UnitsWhatCanDie.Ghost3));
                        }
                        else if (this.model.SpawnedEnemies[i] is EnemyOrc1)
                        {
                            this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[i].Position.X, this.model.SpawnedEnemies[i].Position.Y, UnitsWhatCanDie.Orc1));
                        }
                        else if (this.model.SpawnedEnemies[i] is EnemyOrc2)
                        {
                            this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[i].Position.X, this.model.SpawnedEnemies[i].Position.Y, UnitsWhatCanDie.Orc2));
                        }
                        else if (this.model.SpawnedEnemies[i] is EnemyOrc3)
                        {
                            this.model.DiedItems.Add(new DyingItems(this.model.SpawnedEnemies[i].Position.X, this.model.SpawnedEnemies[i].Position.Y, UnitsWhatCanDie.Orc3));
                        }

                        this.model.Score += this.model.SpawnedEnemies[i].Level * 5;
                        this.model.Gold += this.model.SpawnedEnemies[i].Level * 10;

                        this.model.SpawnedEnemies[i] = null;

                    }

                }

            }

        }

    }
}
