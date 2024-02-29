using System;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using System.Drawing;

 public class Player : AnimationSprite
{
    //private Vector2 velocity = new Vector2();
    bool isOnGround = false;
    
    private float gravity;
    private float speed;
    private int score;
    
    public Player(string filename, int colls, int rows, TiledObject obg = null) : base("sprites/barry.png", 7, 1)
    {
        gravity = .3f;
        speed = 3;
        
        scale=2;  
        SetOrigin(width/2, height/2);

        score = 0;
    }



    public void Update()
    {
        if (Input.GetKey(Key.LEFT))
        {
            MoveUntilCollision(-speed, 0);
            SetCycle(0, 3);
            Mirror(true, false);

        }
        else if (Input.GetKey(Key.RIGHT))
        {
            MoveUntilCollision(speed, 0);
            SetCycle(0, 3);
            Mirror(false, false);
        }
        else
        {
            SetCycle(4, 3);
        }

        //velocity.y += gravity;

        // if(MoveUntilCollision(0, velocity.y) != null)
        // {
        //     velocity.y = 0;
        //     isOnGround = true;
        // }

        // GameObject[] collidingObjects = GetCollisions();
        // foreach (GameObject collidingObject in collidingObjects)
        // {
        //     if (collidingObject is PickUp)
        //     {
        //         collidingObject.LateDestroy();
        //         //Console.WriteLine();
        //     }
        // } 
        
        Animate(0.05f);

    }

    // protected override Collider createCollider()
    // {
    //     EasyDraw baseShape = new EasyDraw(30,60);
    //     baseShape.SetXY(0,-40);
    //     baseShape.Clear(ColorTranslator.FromHtml("#55FF0000"));
    //     AddChild(baseShape);
    //     return new BoxCollider(baseShape);
    // }

    public void addScore(int scoreAmount)
    {
        score += scoreAmount;
        Console.WriteLine(score);
    }

}
