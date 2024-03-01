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
    public float speed { get; private set; }
    private int score;
    private int boundary;
    public Player(string filename, int colls, int rows, TiledObject obg = null) : base("sprites/robotGuy_spritesheet.png", 12, 1 , -1, false, false)
    {
        gravity = .3f;
        speed = 12;
        
        scale=2;  
        SetOrigin(width/2, height/2);

        score = 0;

        boundary = 700;
    }



    public void Update()
    {
        Animate(0.06f);
        if (Input.GetKey(Key.LEFT))
        {
            if (x > boundary)
            {
                x -= speed;
            }
            SetCycle(6, 6);
            Mirror(true, false);

        }
        else if (Input.GetKey(Key.RIGHT))
        {
            if (x < game.width - boundary)
            {
                x += speed;
            }
            SetCycle(6, 6);
            Mirror(false, false);
        }
        else
        {
            SetCycle(0, 6);
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
