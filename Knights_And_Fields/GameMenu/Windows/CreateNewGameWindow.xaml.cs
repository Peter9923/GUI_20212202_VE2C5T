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
using System.Windows.Shapes;

namespace GameMenu.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewGameWindow.xaml
    /// </summary>
    public partial class CreateNewGameWindow : Window
    {
        public CreateNewGameWindow()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            startButton.Background = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\start_hover.png", UriKind.RelativeOrAbsolute)));
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            startButton.Background = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\start_idle.png", UriKind.RelativeOrAbsolute)));
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (playerName.Text != ""){
                GameDisplay.MainWindow game = new GameDisplay.MainWindow(playerName.Text.ToString());
                game.Show();
                this.Close();
            }
        }
    }
}
