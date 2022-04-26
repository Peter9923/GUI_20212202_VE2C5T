using GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Interfaces
{
    public interface IAlliedLogic{

        public void GenerateArrow(int x, int y);
        public void CheckWhichArrowShouldDraw(IUnit allied);
        public void BulletsMoving();
    }
}
