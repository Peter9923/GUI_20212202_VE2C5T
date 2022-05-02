using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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
                DeleteCommand = new RelayCommand(() => {
                    if (SelectedFile != null){
                        File.Delete(SelectedFile.PathName);
                        SavedFiles.Remove(SelectedFile);
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
