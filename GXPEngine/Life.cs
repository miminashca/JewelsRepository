using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Life : Sprite
{
    public Life() : base ("sprites/circle.png")
    {
        scale = 0.5f;
        SetOrigin(width / 2, height / 2);
    }
}
