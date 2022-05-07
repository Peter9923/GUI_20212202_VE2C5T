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
        bool IsCollision(IGameItem other);
        public Point Position { get; set; }
        RectangleGeometry RealArea { get; }
        RectangleGeometry CollisionArea { get; }
    }
}
