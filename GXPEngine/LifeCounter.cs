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
    public int lifeAmount = 3;
    public LifeCounter()
    {
    }

    void Update()
    {
        if (lifeAmount <= 0)
        {
            gameOver = true;
        }
    }

    // Using a public method to change the amount of lives when necessary. Also supports the addition of lives through life packs for example.
    public void ChangeLivesAmount(int amount)
    {
        lifeAmount += amount;
    }
}
