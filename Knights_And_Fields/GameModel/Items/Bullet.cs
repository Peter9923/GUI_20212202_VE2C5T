using GameModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel.Items
{
    public class Bullet : IGameItem
    {
        public double Damage { get; set; }
        public Point Position { get; set; }

        private double speedX;

        public Bullet(double X, double Y, int speed){
            this.speedX = speed;
            this.Position = new Point(X, Y);
            
        }

        public Geometry RealArea
        {
            get
            {
                return new RectangleGeometry(new Rect(Position.X, Position.Y, Config.TileSize, Config.TileSize));
            }
        }

        public Geometry CollisionArea
        {
            get
            {
                return new RectangleGeometry(new Rect(Position.X, Position.Y, Config.TileSize, Config.TileSize));
            }
        }

        public void Move()
        {
            double newX = Position.X + speedX;
            double newY = Position.Y;
            Position = new Point(newX, newY);
        }


        public bool IsCollision(IGameItem other)
        {
            if (other != null)
            {
                return Geometry.Combine(this.CollisionArea, other.CollisionArea,
                 GeometryCombineMode.Intersect, null).GetArea() > 0;
            }
            return false;
        }

        
    }
}
