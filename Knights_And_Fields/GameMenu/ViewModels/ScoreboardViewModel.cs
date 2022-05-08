using GameDisplay;
using GameModel;
using GameModel.Items;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace GameMenu.ViewModels
{
    public class ScoreboardViewModel : ObservableRecipient
    {
        public class Player
        {
            public Player(string name, int score)
            {
                this.Name = name;
                this.Score = score;
            }
            public int Score { get; set; }
            public string Name { get; set; }
        }

        private string[] allSaves;
        public ObservableCollection<Player> Players { get; set; }
        public ICommand ExitCommand { get; set; }
        public event EventHandler OnRequestClose;

        public ScoreboardViewModel()
        {
            Players = new ObservableCollection<Player>();

            if (!IsInDesignMode)
            {
                allSaves = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Saves");
                Model model;

                ICollectionView view = CollectionViewSource.GetDefaultView(Players);
                view.SortDescriptions.Add(new SortDescription("Score", ListSortDirection.Descending));

                foreach (var item in allSaves)
                {
                    model = JsonConvert.DeserializeObject<Model>(File.ReadAllText(item), new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        SerializationBinder = new KnownTypesBinder
                        {
                            KnownTypes = new List<Type>
                                    {
                                        typeof(EnemyGhost),
                                        typeof(EnemyGhost2),
                                        typeof(EnemyGhost3),
                                        typeof(EnemyOrc1),
                                        typeof(EnemyOrc2),
                                        typeof(EnemyOrc3),
                                        typeof(RectangleGeometry),
                                        typeof(Geometry),
                                        typeof(Archer),
                                        typeof(Knight),
                                        typeof(Wall),
                                    }
                        }
                    });

                    Player player = new Player(model.PlayerName, model.Score);
                    Players.Add(player);
                    OnRequestClose(this, new EventArgs());
                }

                ExitCommand = new RelayCommand(() =>
                {
                    OnRequestClose(this, new EventArgs());
                });
            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
