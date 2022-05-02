using GameLogic.Classes;
using GameLogic.Interfaces;
using GameModel;
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

namespace GameDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IModel model;
        string playerName;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        public MainWindow(string PlayerName) : this() {
            playerName = PlayerName;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e){
            this.Left = 0;
            this.Top = 0;
            if (model == null){
                model = new Model(myGrid.ActualHeight, myGrid.ActualWidth, playerName);
            }
            IDisplayLogic displayLogic = new DisplayLogic();
            IButtonLogic buttonLogic = new ButtonLogic(model);
            IAlliedLogic alliedLogic = new AlliedLogic(model);
            IEnemyLogic enemyLogic = new EnemyLogic(model);


            Display display = new Display(model, displayLogic, buttonLogic, alliedLogic, enemyLogic);
            myGrid.Children.Add(display);
        }

        private void Window_LocationChanged(object sender, EventArgs e){
            this.Left = 0;
            this.Top = 0;
        }
    }
}
