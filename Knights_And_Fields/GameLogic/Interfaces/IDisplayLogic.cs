using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLogic.Interfaces
{
    public interface IDisplayLogic
    {
        public Point GetTilePos(Point mousePos);
    }
}
