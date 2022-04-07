using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Game.Models
{
    public interface IGameItem
    {
        public Geometry Area { get; }

        public bool IsCollision(IGameItem other);
        //{
        //    return Geometry.Combine(this.Area, other.Area,
        //        GeometryCombineMode.Intersect, null).GetArea() > 0;
        //}
    }
}
