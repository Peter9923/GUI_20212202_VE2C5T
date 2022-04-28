using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel.Interfaces
{
    public enum UnitsWhatCanDie { 
        Knight,
        Archer,
        Ghost,
        Ghost2,
        Ghost3,
        Orc1,
        Orc2,
        Orc3
    }
    public interface IDyingItems{

        public UnitsWhatCanDie WhoDied { get; set; }
        public Point Position{ get; set; }
        public Geometry RealArea { get; }
        public int DieIndex { get; set; }
    }
}
