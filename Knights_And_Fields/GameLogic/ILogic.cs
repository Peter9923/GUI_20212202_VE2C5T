using GameModel;
using System.Windows;

namespace GameLogic
{
    public interface ILogic
    {
        IModel Model { get; set; }

        Point GetTilePos(Point mousePos);
    }
}