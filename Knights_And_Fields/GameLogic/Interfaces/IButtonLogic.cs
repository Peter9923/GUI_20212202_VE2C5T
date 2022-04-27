using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Interfaces
{
    public interface IButtonLogic{

        public void DeployUnit(int x, int y);
        public void RemoveUnit(int x, int y);

        public void UpgradeUnit(int x, int y);

        public void MoveUnit(int actualX, int actualY, int prevX, int prevY);
    }
}
