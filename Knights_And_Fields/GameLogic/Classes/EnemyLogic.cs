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
    public class EnemyLogic : IEnemyLogic
    {
        static Random rnd = new Random();
        IModel model;

        public EnemyLogic(IModel model)
        {
            this.model = model;
        }

        private void CreateEnemies()
        {
            int enemyCount = this.model.Wave + 2 + rnd.Next(0, this.model.Wave + 10);
            this.model.Wave++;

            for (int i = 0; i < enemyCount; i++)
            {
                int rand = rnd.Next(3, 4);
                if (rand == 1){
                    this.model.ShouldSpawnEnemies.Add(
                    new EnemyGhost((10 * Config.TileSize), ((rnd.Next(0, Config.RowNumbers)) * Config.TileSize), rnd.Next(1, (this.model.Wave + 1)))
                    );
                }
                else if (rand == 2)
                {
                    this.model.ShouldSpawnEnemies.Add(
                    new EnemyGhost2((10 * Config.TileSize), ((rnd.Next(0, Config.RowNumbers)) * Config.TileSize), rnd.Next(1, (this.model.Wave + 1)))
                    );
                }
                else if (rand == 3)
                {
                    this.model.ShouldSpawnEnemies.Add(
                    new EnemyGhost3((10 * Config.TileSize), ((rnd.Next(0, Config.RowNumbers)) * Config.TileSize), rnd.Next(1, (this.model.Wave + 1)))
                    );
                }
            }
        }

        public void DeleteNullEnemies() {
            this.model.SpawnedEnemies = this.model.SpawnedEnemies.Where(x => x != null).ToList();
        }

        public bool SpawnAnEnemy()
        {
            bool result = false;
            if (this.model.ShouldSpawnEnemies.Count == 0 && this.model.SpawnedEnemies.Count == 0)
            {
                CreateEnemies();
                result = true;
            }
            for (int i = 0; i < this.model.ShouldSpawnEnemies.Count; i++){

                bool shouldSpawn = (this.model.SpawnedEnemies.Count == 0 ? true : false);
                int index = 0;
                for (int k = 0; k < this.model.SpawnedEnemies.Count; k++){
                    if (this.model.SpawnedEnemies[k].IsCollision(this.model.ShouldSpawnEnemies[i]))
                    {
                        //then no spawn
                    }
                    else
                    {
                        shouldSpawn = true;
                        index = k;
                        break;
                    }
                }
                if (shouldSpawn){
                    this.model.SpawnedEnemies.Add(this.model.ShouldSpawnEnemies[i]);
                    this.model.ShouldSpawnEnemies[i] = null;
                    break;
                }

            }
            this.model.ShouldSpawnEnemies = this.model.ShouldSpawnEnemies.Where(x => x != null).ToList();
            return result;
        }

        public void EnemyMove() {
            for (int i = 0; i < this.model.SpawnedEnemies.Count; i++){

                if (this.model.SpawnedEnemies[i] != null &&this.model.SpawnedEnemies[i].Position.X <= (Config.TileSize/2)){
                    double proportion = this.model.SpawnedEnemies[i].ActualLife / this.model.SpawnedEnemies[i].MaxLife;
                    this.model.CastleActualHP -= (int)(proportion * 10);

                    //GAME OVER...

                    this.model.SpawnedEnemies[i] = null;
                }
                else{
                    bool wasCollision = false;
                    for (int k = 0; k < this.model.Map.Length; k++){

                        for (int j = 0; j < this.model.Map[k].Length; j++){

                            if (this.model.Map[k][j] != null && this.model.SpawnedEnemies[i] != null && (this.model.SpawnedEnemies[i].IsCollision(this.model.Map[k][j]) == true || this.model.Map[k][j].IsCollision(this.model.SpawnedEnemies[i]) == true)){
                                wasCollision = true;
                                break;
                            }
                            else
                            {
                                wasCollision = false;
                            }
                        }
                        if (wasCollision){
                            break;
                        }
                    }

                    if (this.model.SpawnedEnemies[i] != null)
                    {
                        if (wasCollision)
                        {
                            this.model.SpawnedEnemies[i].ShouldAttack = true;
                            this.model.SpawnedEnemies[i].ShouldWalk = false;
                            this.model.SpawnedEnemies[i].SpeedX = 0;
                        }
                        else
                        {
                            this.model.SpawnedEnemies[i].ShouldAttack = false;
                            this.model.SpawnedEnemies[i].ShouldWalk = true;
                            this.model.SpawnedEnemies[i].SpeedX = -1;
                            this.model.SpawnedEnemies[i].Move();
                        }
                    }
                }
            }
            
            this.DeleteNullEnemies();
        }

        public void AttackAlliedUnits(IEnemy enemy) {

            for (int i = 0; i < this.model.Map.Length; i++)
            {

                for (int j = 0; j < this.model.Map[i].Length; j++)
                {

                    if (this.model.Map[i][j] != null && enemy != null &&
                        (enemy.IsCollision(this.model.Map[i][j]) == true || this.model.Map[i][j].IsCollision(enemy) == true))
                    {
                        this.model.Map[i][j].ActualLife -= enemy.Damage;

                        if (this.model.Map[i][j].ActualLife <= 0)
                        {
                            if (enemy is EnemyGhost || enemy is EnemyGhost2 || enemy is EnemyGhost3){
                                this.model.SOUNDS.GhostKilledAlliedUnit.Open(new Uri("Sounds\\ghostKilledAlliedUnit.mp3", UriKind.RelativeOrAbsolute));
                                this.model.SOUNDS.GhostKilledAlliedUnit.Play();
                            }

                            if (this.model.Map[i][j] is Archer){
                                this.model.DiedItems.Add(new DyingItems((this.model.Map[i][j].Position.X + 1) * Config.TileSize, this.model.Map[i][j].Position.Y * Config.TileSize, UnitsWhatCanDie.Archer));
                            }
                            else if (this.model.Map[i][j] is Knight) {
                                this.model.DiedItems.Add(new DyingItems((this.model.Map[i][j].Position.X + 1) * Config.TileSize, this.model.Map[i][j].Position.Y * Config.TileSize, UnitsWhatCanDie.Knight));
                            }

                            this.model.Map[i][j] = null;

                        }

                    }

                }

            }
        }

    }
}
