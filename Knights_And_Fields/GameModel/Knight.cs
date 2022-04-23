using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel
{
    public class Knight : IAllied
    {
        public int Cost { get { return Config.KnightCost; } }

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
                return new RectangleGeometry(new Rect(Position.X, Position.Y, Config.TileSize-15, Config.TileSize));
            }
        }

        public Knight(int X, int Y){
            this.MaxLife = 100;
            this.ActualLife = MaxLife;
            this.Speed = 2;
            this.Tick = 0;
            this.Position = new Point(X, Y);
            this.Level = 1;
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
