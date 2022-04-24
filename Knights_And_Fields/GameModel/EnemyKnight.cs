using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameModel
{
    public class EnemyKnight : IUnit
    {
        private static Random rnd = new Random();
        public double MaxLife { get { return (Level * 100) / 1.75; } }
        public double ActualLife { get; set; }

        public int Damage { get { return Level * 300; } }

        public int Speed { get; set; }
        public int Tick { get; set; }
        public Point Position { get; set; } //in here not TILE position, pixel position!
        public int Level { get; set; }
        public bool ShouldDie { get; set; }

        public Geometry RealArea
        {
            get
            {
                return new RectangleGeometry(new Rect( Position.X, Position.Y, Config.TileSize, Config.TileSize));
            }
        }

        public Geometry CollisionArea
        {
            get
            {
                return new RectangleGeometry(new Rect(Position.X, Position.Y, Config.TileSize, Config.TileSize));
            }
        }

        private int speedX;

        public EnemyKnight(double X, double Y){
            this.speedX = rnd.Next(-5,0);
            this.Level = 1;

            this.ActualLife = MaxLife;
            this.Speed = 2;
            this.Tick = 0;
            this.Position = new Point(X, Y);
        }


        public void Move() {
            double newX = Position.X + speedX; 
            double newY = Position.Y;
            Position = new Point(newX, newY);
        }

        public bool IsCollision(IGameItem other)
        {
            return Geometry.Combine(this.CollisionArea, other.CollisionArea,
                GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
    }
}
