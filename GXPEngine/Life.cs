using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Life : Sprite
{
    public Sprite lifeEmpty { get; private set; }

    public Life() : base("sprites/battery.png")
    {
        scale = 2;
        SetOrigin(width / 2, height / 2);
        lifeEmpty = new Sprite("sprites/battery_empty.png");
        lifeEmpty.scale = 2;
        lifeEmpty.alpha = 0;
    }
}