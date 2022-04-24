using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel
{
    public class Archer : IAllied
    {
        public int Cost { get { return Config.ArcherCost; } }

        public int UpgradeCost { get { return this.Level * this.Cost; } }

        public double MaxLife { get { return (Level * 100) / 1.1; } }
        public double ActualLife { get; set; }
        public int Damage { get { return this.Level * 8; } }
        public int Speed { get; set; }
        public int Tick { get; set; }
        public Point Position { get; set; }
        public int Level { get; set; }

        public int AnimationIndex { get; set; }

        public Geometry RealArea
        {
            get
            {
                return new RectangleGeometry(new Rect((Position.X + 1) * Config.TileSize, Position.Y * Config.TileSize, Config.TileSize, Config.TileSize));
            }
        }

        public Geometry CollisionArea
        {
            get
            {
                return new RectangleGeometry(new Rect((Position.X + 1) * Config.TileSize, Position.Y * Config.TileSize, Config.TileSize-70, Config.TileSize));
            }
        }

        public Archer(int X, int Y)
        {
            AnimationIndex = 0;
            this.Level = 1;
            this.ActualLife = MaxLife;
            this.Speed = 2;
            this.Tick = 0;
            this.Position = new Point(X, Y);
        }

        public bool IsCollision(IGameItem other)
        {
            return Geometry.Combine(this.CollisionArea, other.CollisionArea,
                GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
    }
}
