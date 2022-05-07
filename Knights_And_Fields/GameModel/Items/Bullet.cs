using GameModel.Interfaces;
using Newtonsoft.Json;
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

        public double speedX { get; set; }


        public Bullet(double X, double Y, int speed){
            this.speedX = speed;
            this.Position = new Point(X, Y);
            
        }
        [JsonIgnore]
        public RectangleGeometry RealArea
        {
            get
            {
                return new RectangleGeometry(new Rect(Position.X, Position.Y, Config.TileSize, Config.TileSize));
            }
        }
        [JsonIgnore]
        public RectangleGeometry CollisionArea
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
