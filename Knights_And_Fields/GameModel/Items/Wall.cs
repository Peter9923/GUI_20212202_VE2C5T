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
    class Wall : IAllied
    {
        public Wall(int X, int Y)
        {
            this.level = 1;
            this.MaxLife = 300;
            this.ActualLife = MaxLife;
            this.Position = new Point(X, Y);
            this.AttackAnimationIndex = 0;
            this.WalkingIndex = 0;
            this.ShouldAttack = false;
            this.ShouldWalk = false;
        }


        public int Cost { get { return 100; } }
        public int UpgradeCost { get { return Level * Cost; } }
        public double MaxLife { get; set; }
        public double ActualLife { get; set; }
        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                MaxLife = level * 100;
                ActualLife = MaxLife;
            }
        }
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
        public double Damage { get { return level * 0; } }
        public Point Position { get; set; }

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
                return new RectangleGeometry(new Rect((Position.X + 1) * Config.TileSize, Position.Y * Config.TileSize, Config.TileSize - (Config.TileSize / 10), Config.TileSize));
            }
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
