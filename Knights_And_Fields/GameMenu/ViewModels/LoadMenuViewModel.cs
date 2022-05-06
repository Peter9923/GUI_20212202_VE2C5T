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
using System.Windows.Input;
using System.Windows.Media;

namespace GameMenu.ViewModels
{
    internal class MySavedFile {
        public string PathName { get; set; }
        public string DisplayName { get; set; }

        public MySavedFile(string Pathname, string Displayname)
        {
            PathName = Pathname;
            DisplayName = Displayname;
        }
    }
    internal class LoadMenuViewModel : ObservableRecipient
    {
        public ObservableCollection<MySavedFile> SavedFiles{ get; private set; }

        //public List<MySavedFile> SavesNames { get; set; }
        private string[] allSaves;

        private MySavedFile selectedFile;

        public MySavedFile SelectedFile
        {
            get { return selectedFile; }
            set 
            {
                OnPropertyChanged();
                SetProperty(ref selectedFile, value);
                (DeleteCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public event EventHandler OnRequestClose;

        public LoadMenuViewModel()
        {
            if (!IsInDesignMode)
            {
                LoadCommand = new RelayCommand(() => {
                    if (SelectedFile != null){
                        if (File.Exists(SelectedFile.PathName)){
                            //Garage result = JsonConvert.DeserializeObject<Garage>(json, new JsonSerializerSettings
                            //{
                            //    TypeNameHandling = TypeNameHandling.Auto
                            //});

                            Model model = JsonConvert.DeserializeObject<Model>(File.ReadAllText(SelectedFile.PathName), new JsonSerializerSettings
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
                            GameDisplay.MainWindow game = new GameDisplay.MainWindow(model);
                            game.ShowDialog();
                            OnRequestClose(this, new EventArgs());
                        }
                    }
                });

                DeleteCommand = new RelayCommand(() => {
                    if (SelectedFile != null){
                        if (MessageBox.Show($"Are you sure you want to delete {SelectedFile.DisplayName}?","DELETE", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes){
                            File.Delete(SelectedFile.PathName);
                            SavedFiles.Remove(SelectedFile);
                        }
                    }
                });

                ExitCommand = new RelayCommand(() => {
                    OnRequestClose(this, new EventArgs());
                });

                
                SavedFiles = new ObservableCollection<MySavedFile>();
                allSaves = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Saves");
                GetSaveNames();
            }
        }


        private void GetSaveNames() {
            for (int i = 0; i < this.allSaves.Length; i++){
                string[] splits = allSaves[i].Split('\\');
                string splits2 = splits[splits.Length - 1].Remove(splits[splits.Length - 1].Length-5);
                SavedFiles.Add(new MySavedFile(allSaves[i], splits2));
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
