using Menu.Model;
using Menu.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Services
{
    internal class OpenNewWindowViaWindow : IOpenNewWindowService
    {
        public void OpenNewWindow(FunctionTypeOfMenu type)
        {
            switch (type)
            {
                case FunctionTypeOfMenu.NewGame: new NewGame().ShowDialog(); break;
                case FunctionTypeOfMenu.LoadGame: new Load().Show() ; break;
                case FunctionTypeOfMenu.Description: new Description().Show(); break;
                case FunctionTypeOfMenu.Scoreboard: new Scoreboard().ShowDialog(); break;
            }
        }
    }
}
