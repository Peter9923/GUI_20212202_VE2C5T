using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Models
{
    public class Knight : IGameItem
    {
        private double centerX;
        private double centerY;

        private int speedX;
        private int speedY;

        private int life;
        private int level;
        private int cost;
        public int Damage { get { return level * 5; } }
        public int UpgradeCost { get { return level * cost; } }

        public Knight(double centerX, double centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
            this.speedX = 0; //cant move
            this.speedY = 0; //cant move

            life = 150;
            level = 1;
            cost = 100;
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



    }
}
