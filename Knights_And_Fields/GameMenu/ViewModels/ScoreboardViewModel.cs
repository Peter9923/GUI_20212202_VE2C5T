using GameModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
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

        public ScoreboardViewModel()
        {
            if (!IsInDesignMode)
            {
                allSaves = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Saves");
                Model model;

                foreach (var item in allSaves)
                {
                    model = JsonConvert.DeserializeObject<Model>(File.ReadAllText(item), new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    });

                    Player player = new Player(model.PlayerName, model.Score);
                    Players.Add(player);
                }
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
