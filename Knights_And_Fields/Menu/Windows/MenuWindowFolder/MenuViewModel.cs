using Menu.Logic;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Menu.ViewModels
{
    internal class MenuViewModel : ObservableRecipient
    {
        IMenuLogic _logic;

        public ICommand NewGameCommand { get; set; }
        public ICommand LoadGameCommand { get; set; }
        public ICommand ScoreboardCommand { get; set; }
        public ICommand DescriptionCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MenuViewModel()
            : this(IsInDesignMode ? null : Ioc.Default.GetService<IMenuLogic>()) { }
        public MenuViewModel(IMenuLogic logic){

            this._logic = logic;

            NewGameCommand = new RelayCommand(
                () => _logic.NewGame()
                ) ;

            LoadGameCommand = new RelayCommand(
                () => _logic.LoadGame()
                );

            ScoreboardCommand = new RelayCommand(
                () => _logic.Scoreboard()
                );

            DescriptionCommand = new RelayCommand(
                () => _logic.Description()
                );

            ExitCommand = new RelayCommand(
                () => _logic.Exit()
                );

            Messenger.Register<MenuViewModel, string, string>(this, "LogicResult", (recipient, msg) => { 
                
            });
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
