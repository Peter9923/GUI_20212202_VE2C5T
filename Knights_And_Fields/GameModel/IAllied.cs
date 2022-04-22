using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public interface IAllied
    {
        public int Cost { get; }
        public int UpgradeCost { get; }
    }
}
