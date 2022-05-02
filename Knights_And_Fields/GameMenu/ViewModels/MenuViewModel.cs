using GameMenu.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GameMenu.ViewModels
{
    internal class MenuViewModel : ObservableRecipient
    {
        MediaPlayer clickSound;
        public ICommand StartCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MenuViewModel()
        {
            clickSound = new MediaPlayer();
            clickSound.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));

            if (!IsInDesignMode){
                StartCommand = new RelayCommand(() => {
                    clickSound.Play();
                    clickSound.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));
                    CreateNewGameWindow newGameWindow = new CreateNewGameWindow();
                    newGameWindow.ShowDialog();
                });

                LoadCommand = new RelayCommand(() => {
                    clickSound.Play();
                    clickSound.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));
                    var vm = new LoadMenuViewModel();
                    var loginWindow = new LoadMenu
                    {
                        DataContext = vm
                    };

                    vm.OnRequestClose += (s, e) => loginWindow.Close();
                    loginWindow.ShowDialog();
                });

                ExitCommand = new RelayCommand(() => {
                    clickSound.Play();
                    clickSound.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));
                    Environment.Exit(0);
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
