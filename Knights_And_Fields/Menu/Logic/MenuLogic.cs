using Menu.Model;
using Menu.Services;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Logic
{
    internal class MenuLogic : IMenuLogic
    {
        IMessenger _messenger;
        IOpenNewWindowService _service;

        public MenuLogic(IMessenger messenger, IOpenNewWindowService service)
        {
            _messenger = messenger;
            _service = service;
        }

        public void NewGame()
        {
            _service.OpenNewWindow(FunctionTypeOfMenu.NewGame);
            _messenger.Send("New Game Window Open", "LogicResult");
        }
        public void LoadGame()
        {
            _service.OpenNewWindow(FunctionTypeOfMenu.LoadGame);
            _messenger.Send("New Load Window Open", "LogicResult");
        }
        public void Scoreboard()
        {
            _service.OpenNewWindow(FunctionTypeOfMenu.Scoreboard);
            _messenger.Send("New Scoreboard Window Open", "LogicResult");
        }
        public void Description()
        {
            _service.OpenNewWindow(FunctionTypeOfMenu.Description);
            _messenger.Send("New Description Window Open", "LogicResult");
        }
        public void Exit()
        {
            Environment.Exit(0);
        }

    }
}
