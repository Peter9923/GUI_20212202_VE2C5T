using Game.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt;
        IGameLogic logic;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Config.Widht = grid.ActualWidth;
            Config.Height = grid.ActualHeight;
            Config.NegyzetHeight = (int)(grid.ActualHeight) / 5;
            Config.NegyzetWidth = (int)(grid.ActualWidth) / 10;


            logic = new GameLogic();

            display.SetUpLogic(logic);
            //logic.RefreshScreen += (obj, args) => InvalidateVisual();
            display.InvalidateVisual();


            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(40);
            dt.Tick += (sender, eventargs) => {
                logic.TimeStep();
                display.InvalidateVisual();
            };

            dt.Start();

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Config.Widht = grid.ActualWidth;
            Config.Height = grid.ActualHeight;
            Config.NegyzetHeight = (int)(grid.ActualHeight) / 5;
            Config.NegyzetWidth = (int)(grid.ActualWidth) / 10;

            display.SetUpLogic(logic);
            display.InvalidateVisual();
        }
    }
}
