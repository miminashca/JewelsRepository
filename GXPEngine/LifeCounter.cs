using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class LifeCounter : GameObject
{
    // Public variable that is read-only, so it can't be modified by other classes.
    public bool gameOver { get; private set; } = false;
    int lifeAmount = 3;
    int previouslifeAmount = 0;
    Life[] lives;
    public LifeCounter()
    {
        lives = new Life[0];
        x = game.width + 35;
        y = 35;
    }

    void Update()
    {
        if (lifeAmount > 0)
        {
            if (previouslifeAmount == 0)
            {
                AddLifeOutlines();
            }
            if (previouslifeAmount != lifeAmount)
            {
                ChangeShownLives();
            }
        }
        else { gameOver = true; }
    }

    void ChangeShownLives()
    {
        for (int i = 1; i <= lifeAmount; i++) 
        {
            Life life = new Life();
            life.x = x - (life.width) * i;
            life.y = y;
            parent.AddChild(life);
        }
        previouslifeAmount = lifeAmount;
    }

    void AddLifeOutlines()
    {
        // Adding the empty life outlines behind the main one's.
        for (int i = 1; i <= lifeAmount; i++)
        {
            Sprite lifeEmpty = new Sprite("sprites/battery_empty.png");
            lifeEmpty.SetOrigin(lifeEmpty.width / 2, lifeEmpty.height / 2);
            lifeEmpty.x = x - (lifeEmpty.width) * i;
            lifeEmpty.y = y;
            lifeEmpty.scaleX = 1.3f;
            lifeEmpty.scaleY = 1.1f;
            parent.AddChild(lifeEmpty);
            Console.WriteLine("Spawning outline at: {0}, {1}", lifeEmpty.x, lifeEmpty.y);
        }
    }

    // Using a public method to change the amount of lives when necessary. Also supports the addition of lives through life packs for example.
    public void ChangeLivesAmount(int amount)
    {
        lifeAmount += amount;
        lives = parent.FindObjectsOfType<Life>();
        foreach (Life life in lives)
        {
            life.Remove();
            life.Destroy();
        }
    }
}
