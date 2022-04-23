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
    }
}