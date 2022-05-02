using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameMenu.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewGameWindow.xaml
    /// </summary>
    public partial class CreateNewGameWindow : Window
    {
        SoundPlayer anotherClick;
        public CreateNewGameWindow()
        {
            InitializeComponent();

            this.MouseDown += CreateNewGameWindow_MouseDown;
            this.Loaded += CreateNewGameWindow_Loaded;

            anotherClick = new SoundPlayer();
            anotherClick.SoundLocation = "Sounds\\AnotherClick.wav";
        }

        private void CreateNewGameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            double WVirtual = SystemParameters.VirtualScreenWidth;
            double HVirtual = SystemParameters.VirtualScreenHeight;

            Left = (WVirtual - this.ActualWidth) - WVirtual / 2;
            Top = 0;
        }

        private void CreateNewGameWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            anotherClick.Play();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            anotherClick.Play();
            if (playerName.Text != ""){
                GameDisplay.MainWindow game = new GameDisplay.MainWindow(playerName.Text.ToString());
                game.Show();
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            anotherClick.Play();
            this.Close();
        }
    }
}
