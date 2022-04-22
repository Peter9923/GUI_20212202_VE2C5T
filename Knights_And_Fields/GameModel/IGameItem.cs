﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameModel
{
    public interface IGameItem
    {
        Geometry Area { get; }
        bool IsCollision(IGameItem other);
    }
}
