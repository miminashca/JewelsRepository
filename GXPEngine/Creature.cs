using GXPEngine;
using GXPEngine.Managers;

public class Creature : GameObject
    {
        private int currentBackground;
        private AnimationSprite creatureSprite;
        private bool moveRight = true;
        public Creature()
        {
            x = -1000;
            y = 200;
            scale = 0.15f;
            
            creatureSprite = new AnimationSprite("sprites/alien_flying_spritesheet.png", 5, 2, -1, false, false);
            AddChild(creatureSprite);
            creatureSprite.SetCycle(0, 8);
        }

        private void Update()
        {
            creatureSprite.Animate(0.1f);
            creatureMove();
        }

        private void creatureMove()
        {
            if (x >= game.width)
            {
                moveRight = false;
            }

            if (x <= -1000)
            {
                moveRight = true;
            }
            
            if (moveRight)
            {
                x+= 4;
                creatureSprite.Mirror(false, false);
            }

            if (!moveRight)
            {
                x-= 4;
                creatureSprite.Mirror(true, false);
            }
        }
        public void changeCreature(int pCurrentBackground)
        {
            currentBackground = pCurrentBackground;
            if (currentBackground == 0)
            {
                creatureSprite.Destroy();
                creatureSprite = new AnimationSprite("sprites/alien_flying_spritesheet.png", 5, 2, -1, false, false);
                AddChild(creatureSprite);
                creatureSprite.SetCycle(0, 8);
            }
            else if (currentBackground == 1)
            {
                creatureSprite.Destroy();
                creatureSprite = new AnimationSprite("sprites/alien_flying_spritesheet.png", 5, 2, -1, false, false);
                AddChild(creatureSprite);
                creatureSprite.SetCycle(0, 8);
            }
            else
            {
                creatureSprite.Destroy();
                creatureSprite = new AnimationSprite("sprites/Flying_alien_animation_night.png", 5, 2, -1, false, false);
                AddChild(creatureSprite);
                creatureSprite.SetCycle(0, 8);
            }
        }
    }
