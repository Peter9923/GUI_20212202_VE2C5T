using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel
{
    class Knight : IAllied
    {
        public int Cost => throw new NotImplementedException();

        public int UpgradeCost => throw new NotImplementedException();

        public double MaxLife { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double ActualLife { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Damage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Tick { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Geometry Area => throw new NotImplementedException();

        public void Collision()
        {
            throw new NotImplementedException();
        }

        public bool IsCollision(IGameItem other)
        {
            throw new NotImplementedException();
        }
    }
}
