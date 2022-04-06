using Menu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Services
{
    internal interface IOpenNewWindowService
    {
        void OpenNewWindow(FunctionTypeOfMenu type);
    }
}
