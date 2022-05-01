using GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel.Items
{
    class Wall : IAllied
    {
        public int UpgradeCost => throw new NotImplementedException();

        public int Cost => throw new NotImplementedException();

        public double MaxLife { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double ActualLife { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Level { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AttackAnimationIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int WalkingIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ShouldAttack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ShouldWalk { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double Damage => throw new NotImplementedException();

        public Point Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Geometry RealArea => throw new NotImplementedException();

        public Geometry CollisionArea => throw new NotImplementedException();

        public bool IsCollision(IGameItem other)
        {
            throw new NotImplementedException();
        }
    }
}
