using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class LifeCounter : GameObject
{
    int lifeAmount = 3;
    int previouslifeAmount = 0;
    public LifeCounter()
    {
        x = game.width;
        y = 25;
    }

    void Update()
    {
        if (previouslifeAmount != lifeAmount)
        {
            Console.WriteLine("Lives changed.");
            ChangeShownLives();
        }
    }

    void ChangeShownLives()
    {
        for (int i = 1; i <= lifeAmount; i++) 
        {
            Life life = new Life();
            life.x = x - (life.width * 1.5f) * i;
            life.y = y;
            parent.AddChild(life);
        }
        previouslifeAmount = lifeAmount;
    }

    // Using a public method to change the amount of lives when necessary. Also supports the addition of lives through life packs for example.
    public void ChangeLivesAmount(int amount)
    {
        lifeAmount += amount;
    }
}
