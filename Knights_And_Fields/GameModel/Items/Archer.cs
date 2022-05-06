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
    public class Archer : IAllied {
        public Archer(int X, int Y)
        {
            this.level = 1;
            this.MaxLife = 100;
            this.ActualLife = MaxLife;
            this.Position = new Point(X, Y);
            this.AttackAnimationIndex = 0;
            this.WalkingIndex = 0;
            this.ShouldAttack = true;
            this.Arrows = new List<Bullet>();
        }

        public int Cost { get { return 250; } }
        public int UpgradeCost { get { return (Level * Cost); } }
        public double MaxLife { get; set; }
        public double ActualLife { get; set; }

        private int level;
        public int Level { get { return level; } set {
                level = value;
                MaxLife = level * 100;
                ActualLife = MaxLife;
            } }
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
        public double Damage { get { return (level * 10); }}
        public Point Position { get; set; }

        [JsonIgnore]
        public RectangleGeometry RealArea
        {
            get
            {
                return new RectangleGeometry(new Rect((Position.X + 1) * Config.TileSize, Position.Y * Config.TileSize, Config.TileSize, Config.TileSize));
            }
        }

        [JsonIgnore]
        public RectangleGeometry CollisionArea
        {
            get
            {
                return new RectangleGeometry(new Rect((Position.X + 1) * Config.TileSize, Position.Y * Config.TileSize, (Config.TileSize/4)*3, Config.TileSize));
            }
        }

        public void AddArrow(Bullet arrow) {
            if (this.Arrows.Count < 2){
                Arrows.Add(arrow);
            }
        }

        public bool IsCollision(IGameItem other)
        {
            if (other != null){
                return Geometry.Combine(this.CollisionArea, other.CollisionArea,
                 GeometryCombineMode.Intersect, null).GetArea() > 0;
            }
            return false;
        }

        public List<Bullet> Arrows { get; set; }
    }
}
