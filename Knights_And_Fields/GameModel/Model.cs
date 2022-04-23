using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Model : IModel
    {
        public Model(double h, double w)
        {
            Config.GameHeight = h;
            Config.GameWidth = w;

            this.Map = new IUnit[Config.RowNumbers][];
            for (int i = 0; i < this.Map.Length; i++)
            {
                this.Map[i] = new IUnit[Config.ColumnNumbers];
            }

            this.Gold = 1000000;
            this.Wave = 1;
            this.CastleMaxHP = 500;
            this.CastleActualHP = this.CastleMaxHP;
            this.Score = 0;
            Config.TileSize = 160;
            this.DeployKnight = false;

            ShouldSpawnEnemies = new List<EnemyKnight>();
            SpawnedEnemies = new List<EnemyKnight>();
        }
        public IUnit[][] Map { get; set; }
        public List<EnemyKnight> ShouldSpawnEnemies {get; set;}
        public List<EnemyKnight> SpawnedEnemies {get; set;}

        public int Gold { get; set; }
        public int Wave { get; set; }
        public int CastleMaxHP { get; set; }
        public int CastleActualHP { get; set; }
        public int Score { get; set; }
        public bool DeployKnight { get; set; }
        public bool MoveUnit { get; set; }
        public bool RemoveUnit { get; set; }
        public bool UpgradeUnit { get; set; }

    }
}
