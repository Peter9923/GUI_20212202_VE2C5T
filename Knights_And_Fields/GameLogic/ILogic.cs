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

        public void ClickSound(bool selection);

        void GenerateArrow(int x, int y);

        bool EnemyAndAlliedUnitMetEachOther(EnemyKnight enemy, IAllied allied);
        void EnemyIsInTheCastle(EnemyKnight enemy);
        int EnemySpawnTime();

        void CheckCollision(bool shouldAttack);
        void CheckEnemyIsDied();

        void CheckWhichArrowShouldDraw(IUnit allied);
    }
}