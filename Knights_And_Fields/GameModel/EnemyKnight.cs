using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel
{
    public class EnemyKnight : IUnit
    {
        public double MaxLife { get { return Math.Round((Level * 100) / 1.75,2); } }
        public double ActualLife { get; set; }

        public int Damage { get { return Level * 10; } }

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

        private int speedX;

        public EnemyKnight(double X, double Y){
            this.speedX = -5;
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
