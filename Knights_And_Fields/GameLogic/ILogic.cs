using GameModel;
using System.Windows;

namespace GameLogic
{
    public interface ILogic
    {
        IModel Model { get; set; }

        Point GetTilePos(Point mousePos);

        public void DeployKnight(int x, int y);
        public void RemoveKnight(int x, int y);
        public void UpgradeKnight(int x, int y);
        public void MoveKnight(int actualX, int actualY, int prevX, int prevY);

        void EnemyAndAlliedUnitMetEachOther(EnemyKnight enemy, IAllied allied);
        void EnemyIsInTheCastle(EnemyKnight enemy);
        int EnemySpawnTime();
    }
}