using Game.Models;
using System.Collections.Generic;

namespace Game.Logic
{
    public interface IGameLogic
    {
        List<Enemy> Enemies { get; set; }
        List<Knight> Knights { get; set; }

        void AddEnemy(double x, double y);
        void AddKnight(double x, double y);
        Enemy EnemyBuilder(List<Enemy> others, double X, double Y);
        Knight KnightBuilder(List<Knight> others, double X, double Y);
        bool ClickIsRightPosition(double X, double Y);
        void TimeStep();
    }
}