using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel.Interfaces
{
    public interface IUnit : IGameItem{
        public double MaxLife { get; set; }
        public double ActualLife { get; set; }
        public int Level { get; set; }
        public int AttackAnimationIndex { get; set; }
        public int WalkingIndex { get; set; }
        public bool ShouldAttack { get; set; }
        public bool ShouldWalk { get; set; }
    }
}
