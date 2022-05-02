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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        MediaPlayer clickSound;
        public Menu()
        {
            clickSound = new MediaPlayer();
            clickSound.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));
            InitializeComponent();
        }

        private void playClick() {
            clickSound.Play();
            clickSound.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            playClick();
        }
    }
}
