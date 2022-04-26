using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel.Interfaces
{
    public interface IEnemy : IUnit
    {
        public double SpeedX { get; set; }
        public void Move();
    }
}
