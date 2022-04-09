using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Services
{
    public interface ICreateNewKnightService
    {
        void OpenKnightCreatorWindow(ref ILivingGameItem knight);
    }
}
