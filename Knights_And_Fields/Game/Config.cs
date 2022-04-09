using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Game
{
    public static class Config
    {
        public static Brush BorderBrush = Brushes.DarkGray;
        public static Brush BgBrush = Brushes.Cyan;

        public static Brush EnemyBgBrush = Brushes.DarkRed;
        public static Brush EnemyLineBrush = Brushes.Black;

        public static Brush Knight_KnightBG = Brushes.LightGray;
        public static Brush Knight_KnightLB = Brushes.Black;

        public static Brush Knight_TowerBG = Brushes.Black;
        public static Brush Knight_TowertLB = Brushes.YellowGreen;

        public static Brush Knight_ArcherBG = Brushes.LightSeaGreen;
        public static Brush Knight_ArcherLB = Brushes.Black;

        public static Brush Knight_CannonBG = Brushes.SaddleBrown;
        public static Brush Knight_CannontLB = Brushes.Black;


        public static double Widht = 700;
        public static double Height = 300;
        public static double RowNumber = 5;
        public static double ColumnNumber = 10;

        public static int BorderSize = 0;
        public static int NegyzetHeight = 100;
        public static int NegyzetWidth = 100;
    }
}
