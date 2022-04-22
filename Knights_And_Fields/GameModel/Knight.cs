using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel
{
    class Knight : IAllied
    {
        public int Cost { get { return 250; } }

        public int UpgradeCost { get { return this.Level * this.Cost; } }

        public double MaxLife { get; set; }
        public double ActualLife { get; set; }
        public int Damage { get { return this.Level * 10; } }
        public int Speed { get; set; }
        public int Tick { get; set; }
        public Point Position { get; set; }
        public int Level { get; set; }

        public Geometry Area
        {
            get
            {
                return new RectangleGeometry(new Rect(Position.X, Position.Y, Config.TileSize, Config.TileSize));
            }
        }

        public void Collision()
        {
            throw new NotImplementedException();
        }

        public bool IsCollision(IGameItem other)
        {
            throw new NotImplementedException();
        }
    }
}
