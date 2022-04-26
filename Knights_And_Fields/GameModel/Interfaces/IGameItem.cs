using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel.Interfaces
{
    public interface IGameItem{
        double Damage { get; }
        public Point Position { get; set; }
        Geometry RealArea { get; }
        Geometry CollisionArea { get; }
        bool IsCollision(IGameItem other);
    }
}
