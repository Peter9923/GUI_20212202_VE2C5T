using GameModel.Interfaces;
using GameModel.Items;
using System.Collections.Generic;

namespace GameModel
{
    public interface IModel
    {
        public MySounds SOUNDS { get; set; }
        int CastleActualHP { get; set; }
        int CastleMaxHP { get; set; }
        bool DeployArcher { get; set; }
        bool DeployKnight { get; set; }
        int Gold { get; set; }
        IUnit[][] Map { get; set; }
        public List<IDyingItems> DiedItems { get; set; }
        bool MoveUnit { get; set; }
        string PlayerName { get; set; }
        bool RemoveUnit { get; set; }
        int Score { get; set; }
        List<IEnemy> ShouldSpawnEnemies { get; set; }
        List<IEnemy> SpawnedEnemies { get; set; }
        bool UpgradeUnit { get; set; }
        int Wave { get; set; }
    }
}