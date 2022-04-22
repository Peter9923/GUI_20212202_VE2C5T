using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameModel
{
    public interface IUnit : IGameItem
    {
        double MaxLife { get; set; }
        double ActualLife { get; set; }
        int Damage { get; set; }
        int Speed { get; set; }
        int Tick { get; set; }
        Point Position { get; set; }
        public void Collision();
    }
}
