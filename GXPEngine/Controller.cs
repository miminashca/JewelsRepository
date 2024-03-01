using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using System.Drawing;
using System.Net.Mime;
using System.Reflection;
using System.Collections;

public class Controller : Sprite
{
    private Player player;
    private int turnSpeed = 1;
    public Controller(Player pPlayer) : base("sprites/platform.png")
    {
        player = pPlayer;
        // width = 70;
        // height = 20;
        SetOrigin(width/2, height/2);
        scale = 2;
           
        collider.isTrigger = true;
    }

    void Update()
    {
        x = player.x;
        y = player.y;
    }

    public float GetRotation()
    {
        return rotation;
    }
    
    protected override Collider createCollider()
    {
        EasyDraw baseShape = new EasyDraw(100,20);
        baseShape.SetXY(-50,-10);
        //baseShape.Clear(ColorTranslator.FromHtml("#55FF0000"));
        AddChild(baseShape);
        return new BoxCollider(baseShape);
    }
        
}