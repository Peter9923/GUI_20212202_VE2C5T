using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Game.Models
{
    public interface ILivingGameItem : IGameItem
    {
        public TypeOfKnights Type { get; set; }
        double MaxLife { get; set; }
        double ActualLife { get; set; }
        int Cost { get; }
        public int Damage { get; }
        public int UpgradeCost { get; }

        public Geometry Area { get; }

        public bool IsCollision(IGameItem other);

        public void SetXY(double x, double y);
        public void Collision();
    }
}
