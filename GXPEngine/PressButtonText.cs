using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class PressButtonText : Sprite
{
    public PressButtonText(TiledObject obj = null) : base("sprites/start_text_white.png", false, false)
    {
        obj.Initialize();
        SetOrigin(width / 2, height / 2);
        visible = false;
    }
}
