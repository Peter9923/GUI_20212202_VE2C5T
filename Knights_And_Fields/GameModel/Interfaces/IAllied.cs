using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel.Interfaces
{
    public interface IAllied : IUnit{
        public int Cost { get; set; }
        public int UpgradeCost { get; }
    }
}
