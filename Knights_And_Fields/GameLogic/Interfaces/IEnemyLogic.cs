using GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Interfaces
{
    public interface IEnemyLogic
    {
        public bool SpawnAnEnemy();
        public void DeleteNullEnemies();
        public void EnemyMove();
        public void AttackAlliedUnits(IEnemy enemy);


    }
}
