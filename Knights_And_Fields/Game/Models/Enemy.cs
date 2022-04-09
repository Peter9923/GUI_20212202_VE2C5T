using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Models
{
    public class Enemy : IGameItem
    {
        private double centerX;
        private double centerY;

        private int speedX;
        private int speedY;

        private int life;
        private int level;
        public int Damage { get { return level * 5; } }

        public Enemy(double centerX, double centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
            this.speedX = 0; //cant move
            this.speedY = 0; //cant move

            speedX = -5;
            speedY = 0;

            life = 100;
            level = 1;
         
        }

        public Geometry Area
        {
            get
            {
                return new RectangleGeometry(new Rect(centerX, centerY, Config.NegyzetWidth, Config.NegyzetHeight));
            }
        }

        public bool IsCollision(IGameItem other)
        {
            return Geometry.Combine(this.Area, other.Area,
                GeometryCombineMode.Intersect, null).GetArea() > 0;
        }

        public void SetXY(double x, double y)
        {
            this.centerX = x;
            this.centerY = y;
        }

        //what happen when collision
        public void Collision()
        {
            life -= 10;
        }

        public void Move()
        {
            centerX += speedX;
            centerY += speedY;
        }
    }
}
