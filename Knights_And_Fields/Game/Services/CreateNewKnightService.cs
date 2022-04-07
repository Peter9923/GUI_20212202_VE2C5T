using Game.Models;
using Game.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Services
{
    public class CreateNewKnightService : ICreateNewKnightService
    {
        public void OpenKnightCreatorWindoe(Knight knight)
        {
            new KnightCreatorWindow(knight).ShowDialog();
        }
    }
}
