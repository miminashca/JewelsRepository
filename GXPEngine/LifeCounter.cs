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
            ShowLives();
        }
    }

    void ShowLives()
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

}
