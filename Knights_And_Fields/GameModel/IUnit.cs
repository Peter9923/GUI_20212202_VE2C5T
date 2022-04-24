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
        double MaxLife { get; }
        double ActualLife { get; set; }
        int Damage { get; }
        int Speed { get; set; }
        int Tick { get; set; }
        Point Position { get; set; }
        int Level { get; set; }
    }
}
