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
        ILivingGameItem Knight;
        public KnightCreatorWindow()
        {
            InitializeComponent();
        }

        public KnightCreatorWindow(ILivingGameItem knight) : this()
        {
            Knight = knight;
        }

       
        private void AddKnightClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button b){
                if (b.Content.ToString() == "Add Knight"){
                    Knight.Type = TypeOfKnights.Knight;
                }
                else if (b.Content.ToString() == "Add Archer"){
                    Knight.Type = TypeOfKnights.Archer;
                }
                else if (b.Content.ToString() == "Add Cannon"){
                    Knight.Type = TypeOfKnights.Cannon;
                }
                else if (b.Content.ToString() == "Add Tower"){
                    Knight.Type = TypeOfKnights.Tower;
                }
            }
            DialogResult = true;
        }
    }
}
