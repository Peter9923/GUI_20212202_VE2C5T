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
            this.CastleHP = 500;
            this.Score = 0;
            Config.TileSize = 160;
            this.DeployKnight = false;
        }

        public int Gold { get; set; }
        public int Wave { get; set; }
        public int CastleHP { get; set; }
        public int Score { get; set; }
        public IUnit[][] Map { get; set; }
        public bool DeployKnight { get; set; }

    }
}
