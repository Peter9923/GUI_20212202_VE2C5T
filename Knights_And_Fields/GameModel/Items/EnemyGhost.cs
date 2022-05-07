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
    public class EnemyGhost : IEnemy
    {
        public EnemyGhost(double X, double Y, int level){
            this.Level = level;
            this.MaxLife = level * 100;
            this.ActualLife = MaxLife;
            this.Position = new Point(X, Y);
            this.Damage = level * 5;

            this.ShouldWalk = true;
            this.AttackAnimationIndex = 0;
            this.WalkingIndex = 0;

            speedX = -1.25;
            
        }
      

        private static Random rnd = new Random();
        private double speedX;
        public double SpeedX { get { return speedX; } set { speedX = value; } }
        public double MaxLife { get; set; }
        public double ActualLife { get; set; }
        public int Level { get; set; }

        private bool shouldAttack;
        public bool ShouldAttack
        {
            get { return shouldAttack; }
            set
            {
                shouldAttack = value;
                if (shouldAttack)
                {
                    shouldWalk = false;
                }
            }
        }
        public int AttackAnimationIndex { get; set; }
        private bool shouldWalk;
        public bool ShouldWalk
        {
            get { return shouldWalk; }
            set
            {
                shouldWalk = value;
                if (shouldWalk)
                {
                    shouldAttack = false;
                }
            }
        }
        public int WalkingIndex { get; set; }
        
        public double Damage { get; set; }
        public Point Position { get; set; }

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
                return new RectangleGeometry(new Rect(Position.X + (Config.TileSize/2), Position.Y, Config.TileSize - (Config.TileSize / 2), Config.TileSize));
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
