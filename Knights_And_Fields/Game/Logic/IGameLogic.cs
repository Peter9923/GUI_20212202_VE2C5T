using Game.Models;
using System.Collections.Generic;

namespace Game.Logic
{
    public interface IGameLogic
    {
        List<Enemy> Enemies { get; set; }
        List<ILivingGameItem> Knights { get; set; }

        void AddEnemy(double x, double y);
        void AddKnight(double x, double y);
        bool ClickIsRightPosition(double X, double Y);
        void CreateOrUpgradeKnight(double X, double Y);
        Enemy EnemyBuilder(List<Enemy> others, double X, double Y);
        ILivingGameItem KnightBuilder(List<ILivingGameItem> others, double X, double Y);
        void TimeStep();
    }
}