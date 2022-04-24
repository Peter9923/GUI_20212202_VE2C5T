﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GameModel
{
    public class Knight : IAllied
    {
        public int Cost { get { return Config.KnightCost; } }

        public int UpgradeCost { get { return this.Level * this.Cost; } }

        public double MaxLife { get { return (Level * 100) / 1.5; } }
        public double ActualLife { get; set; }
        public int Damage { get { return this.Level * 10; } }
        public int Speed { get; set; }
        public int Tick { get; set; }
        public Point Position { get; set; }
        public int Level { get; set; }

        public Geometry RealArea
        {
            get
            {
                return new RectangleGeometry(new Rect(( Position.X+1) *Config.TileSize, Position.Y*Config.TileSize, Config.TileSize, Config.TileSize));
            }
        }

        public Geometry CollisionArea => throw new NotImplementedException();

        public Knight(int X, int Y){
            this.Level = 1;
            this.ActualLife = MaxLife;
            this.Speed = 2;
            this.Tick = 0;
            this.Position = new Point(X, Y);
        }

        public bool IsCollision(IGameItem other)
        {
            return Geometry.Combine(this.CollisionArea, other.CollisionArea,
                GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
    }
}
