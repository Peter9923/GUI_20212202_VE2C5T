using Game.Models;
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

namespace Game.Windows
{
    /// <summary>
    /// Interaction logic for KnightCreatorWindow.xaml
    /// </summary>
    public partial class KnightCreatorWindow : Window
    {
        Knight Knight;
        public KnightCreatorWindow()
        {
            InitializeComponent();
        }

        public KnightCreatorWindow(Knight knight) : this()
        {
            Knight = knight;
            Knight = null;
        }

        private void AddKnightClick(object sender, RoutedEventArgs e)
        {
            Knight = new Knight(0, 0);
            DialogResult = true;
        }
    }
}
