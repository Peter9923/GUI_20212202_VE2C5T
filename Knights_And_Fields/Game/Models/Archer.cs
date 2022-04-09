using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Models
{
    public class Archer : ILivingGameItem
    {
        private double centerX;
        private double centerY;

        private int speedX;
        private int speedY;
        private int level;

        public double MaxLife { get; set; }
        public double ActualLife { get; set; }
        public int Cost { get { return 100; } }
        public int Damage { get { return level * 5; } }
        public int UpgradeCost { get { return level * Cost; } }

        public Archer(double centerX, double centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
            this.speedX = 0; //cant move
            this.speedY = 0; //cant move

            MaxLife = 10;
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
            ActualLife -= 10;
        }
    }
}
