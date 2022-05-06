using GameModel.Interfaces;
using GameModel.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Model : IModel
    {

        public Model(double h, double w, string PlayerName)
        {
            Config.GameHeight = h;
            Config.GameWidth = w;

            this.Map = new IUnit[Config.RowNumbers][];
            for (int i = 0; i < this.Map.Length; i++)
            {
                this.Map[i] = new IUnit[Config.ColumnNumbers];
            }

            double tileSize1 = Config.GameWidth / (Config.ColumnNumbers+1);
            double tileSize2 = Config.GameHeight / (Config.RowNumbers+2);
            Config.TileSize = (tileSize1 >= tileSize2) ? tileSize2 : tileSize1;

            this.Gold = 1200;
            this.Wave = 0;
            this.CastleMaxHP = 500;
            this.CastleActualHP = this.CastleMaxHP;
            this.Score = 0;
            this.DeployKnight = false;
            this.PlayerName = PlayerName;
            ShouldSpawnEnemies = new List<IEnemy>();
            SpawnedEnemies = new List<IEnemy>();
            DiedItems = new List<IDyingItems>();
            SOUNDS = new MySounds();
        }


        public IUnit[][] Map { get; set; }

        public List<IEnemy> ShouldSpawnEnemies { get; set; }
       
        public List<IEnemy> SpawnedEnemies { get; set; }

        public List<IDyingItems> DiedItems { get; set; }

        public MySounds SOUNDS { get; set; }
        public string PlayerName { get; set; }
        public int Gold { get; set; }
        public int Wave { get; set; }
        public int CastleMaxHP { get; set; }
        public int CastleActualHP { get; set; }
        public int Score { get; set; }
        public bool DeployKnight { get; set; }
        public bool DeployArcher { get; set; }
        public bool DeployWall { get; set; }
        public bool MoveUnit { get; set; }
        public bool RemoveUnit { get; set; }
        public bool UpgradeUnit { get; set; }
        public bool SelectedSave { get; set; }

    }
}
