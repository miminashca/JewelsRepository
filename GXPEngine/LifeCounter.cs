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
        x = game.width + 100;
        y = 0;
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
            life.y = y + life.height * 1.2f;
            parent.AddChild(life);
        }
        previouslifeAmount = lifeAmount;
    }

    void AddLifeOutlines()
    {
        // Adding the empty life outlines behind the main one's.
        for (int i = 1; i <= lifeAmount; i++)
        {
            Life life = new Life();
            life.lifeEmpty.x = x - (life.width) * i;
            life.lifeEmpty.y = y + (life.height * 1.2f);
            life.lifeEmpty.alpha = 1;
            life.alpha = 0;
            parent.AddChild(life.lifeEmpty);
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
