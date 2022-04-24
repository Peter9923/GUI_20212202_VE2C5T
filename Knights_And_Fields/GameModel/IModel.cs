using System.Collections.Generic;

namespace GameModel
{
    public interface IModel
    {
        IUnit[][] Map { get; set; }
        List<EnemyKnight> ShouldSpawnEnemies { get; set; }
        List<EnemyKnight> SpawnedEnemies { get; set; }
        public int CastleMaxHP { get; set; }
        public int CastleActualHP { get; set; }
        bool DeployKnight { get; set; }
        bool DeployArcher { get; set; }
        int Gold { get; set; }
        int Score { get; set; }
        int Wave { get; set; }
        bool MoveUnit { get; set; }
        bool RemoveUnit { get; set; }
        bool UpgradeUnit { get; set; }
    }
}