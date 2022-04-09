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

        public static Brush Player_KatonaBgBrush = Brushes.LightBlue;
        public static Brush Player_KatonaLineBrush = Brushes.Black;

        public static Brush Player_FalBgBrush = Brushes.Black;
        public static Brush Player_FalLineBrush = Brushes.Black;

        public static Brush Player_AgyuBgBrush = Brushes.SaddleBrown;
        public static Brush Player_AgyuLineBrush = Brushes.Black;

        public static Brush Player_IjaszBgBrush = Brushes.LightYellow;
        public static Brush Player_IjaszLineBrush = Brushes.Black;


        public static double Widht = 700;
        public static double Height = 300;
        public static double RowNumber = 5;
        public static double ColumnNumber = 10;

        public static int BorderSize = 0;
        public static int NegyzetHeight = 100;
        public static int NegyzetWidth = 100;
    }
}
