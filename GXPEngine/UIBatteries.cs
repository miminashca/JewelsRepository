using System;
using System.Collections;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using System.Drawing;

public class UIBatteries : GameObject
    {
        private Sprite batteryImage;
        private Sprite[] batteriesList;
        
        private float batteriesCoordinate;
        private int currentLives;

        private LifeCounter lifecounter;
        
        public UIBatteries(LifeCounter pLifeCounter)
        {
            lifecounter = pLifeCounter;
            
            batteriesList = new Sprite[3];
            
            currentLives = lifecounter.lifeAmount;
           
            createBatteries();
        }

        void Update()
        {
            if (lifecounter.lifeAmount < currentLives || lifecounter.lifeAmount > currentLives)
            {
                for (int i = 0; i < batteriesList.Length; i++)
                {
                    batteriesList[i].Destroy();
                }

                currentLives = lifecounter.lifeAmount;
                createBatteries();
            }
        }

        private void createBatteries()
        {
            for (int i = 0; i < batteriesList.Length; i++)
            {
                if (i+1 <= lifecounter.lifeAmount)
                {
                    batteryImage = new Sprite("sprites/battery.png", false, false);
                }
                else
                {
                    batteryImage = new Sprite("sprites/battery_empty.png", false, false);
                }
                batteryImage.scale = 2f;
                batteriesCoordinate = game.width*9 / 10 - batteryImage.width * 1.5f;
                batteryImage.SetXY(batteriesCoordinate + batteryImage.width*i, 0);

                batteriesList[i] = batteryImage;
                AddChild(batteriesList[i]);
            }
        }

    }
