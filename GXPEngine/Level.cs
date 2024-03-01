using System;
using System.Collections;
using GXPEngine.Core;
using System.Drawing;
using TiledMapParser;
using GXPEngine;
public class Level : GameObject
{
    public bool levelCleaned = false;
    private Player player;
    public Gem gem;
    private Box box;
    private RotationReader rotationReader;
    
    private ArduinoControls arduinoControls;
    private LifeCounter lifeCounter;
    private PressButtonText pressButtonText;
    int currentTextTime;
    int textBlinkTime = 1250;
    int highScore;
	
    private int currentGemTime;
    private ArrayList gems;
	
    private int gemDestructionPoint;
    public float gemSpawnTime;
	
    private Controller controller;
    private UIBatteries batteries;

    private Sprite background1;
    private Sprite background2;
    private Sprite background3;
    private Sprite[] backgrounds;

    private int currentTime;
    private int backgroundSwitchTimeMs;
    
    float fadeTime;
    private bool fade = false;
    //boolean fadeSwitched = false;
    private float maxAlpha = 1;
    private float minAlpha = 0;
    private float currentAlphaDown;
    private float currentAlphaUp;

    private int currentBackground = 0;
    
    public Sound DropSound;
    public SoundChannel musicChannelDrop;

    private Random random;
    private float randomVol;

    private Creature creature;
    public Level(string mapName)
    {
        gemDestructionPoint = game.height;
        gemSpawnTime = 3;
		
        gems = new ArrayList();
        currentGemTime = Time.time;
        currentTextTime = Time.time;
        TiledLoader loader = new TiledLoader(mapName);
        loader.rootObject = this;
        loader.LoadTileLayers(0);
        loader.autoInstance = true;
        loader.LoadObjectGroups(0);
        loader.LoadObjectGroups(1);
        loader.LoadObjectGroups(2);
        player = FindObjectOfType<Player>();
        pressButtonText = FindObjectOfType<PressButtonText>();

        controller = new Controller(player);
        AddChild(controller);
		
        rotationReader = new RotationReader(controller);
        AddChild(rotationReader);

        arduinoControls = new ArduinoControls();
        AddChild(arduinoControls);

        lifeCounter = new LifeCounter();
        AddChild(lifeCounter);

        box = FindObjectOfType<Box>();

        batteries = new UIBatteries(lifeCounter);
        AddChild(batteries);

        backgrounds = new Sprite[3];

        background1 = new Sprite("sprites/background_day.png", false, false);
        background1.alpha = 1;
        background2 = new Sprite("sprites/background_dusk.png", false, false);
        background2.alpha = 0;
        background3 = new Sprite("sprites/background_night.png", false, false);
        background3.alpha = 0;
        
        backgrounds[0] = background1;
        backgrounds[1] = background2;
        backgrounds[2] = background3;
        
         for (int i = 0; i < backgrounds.Length; i++)
         {
             AddChildAt(backgrounds[i], 0);
         }
        
        currentTime = Time.time;
        backgroundSwitchTimeMs = 15000;

        DropSound = new Sound("sounds/drop.wav");
        random = new Random();
        randomVol = random.Next(2, 6);

        creature = new Creature();
        AddChild(creature);
    }

    public void Update()
    {

        // Stopping action after the player has lost
        if (!lifeCounter.gameOver)
        {
            arduinoControls.UseFile(controller, player);

            if (Time.time - currentGemTime >= gemSpawnTime * 1000 && levelCleaned == false)
            {
                gem = new Gem(rotationReader, player, box, this);
                gems.Add(gem);
                currentGemTime = Time.time;
            }

            foreach (Gem gem in gems.ToArray())
            {
                AddChild(gem);
                if (gem.y >= gemDestructionPoint)
                {
                    musicChannelDrop = DropSound.Play(false, 0U, randomVol / 10);
                    
                    gems.Remove(gem);
                    gem.Destroy();
                    lifeCounter.ChangeLivesAmount(-1);
                }
                //here
                if (gem.collidedBox)
                {
                    gems.Remove(gem);
                    gem.Destroy();
                }
            }
        }
        else if (!levelCleaned) { 
            // Cleaning the level once after the player has failed. 
            cleanLevel();
        }
        else
        {
            if (Time.time - currentTextTime >= textBlinkTime)
            {
                // Blink text at a specific interval before starting a new run
                pressButtonText.visible = !pressButtonText.visible;
                currentTextTime = Time.time;
            }
        }

        UpdateBackground();
    }

    public void cleanLevel()
    {
        foreach (Gem gem in gems.ToArray())
        {
              gems.Remove(gem);
              gem.Destroy();
        }
        levelCleaned = true;
    }

    private void UpdateBackground()
    {
        if (Time.time - currentTime >= backgroundSwitchTimeMs)
        {
            fadeTime = Time.time;
            if (currentBackground < 2)
            {
                currentBackground++;
                Console.WriteLine(currentBackground);
            }
            else
            {
                currentBackground = 0;
                Console.WriteLine(currentBackground);
            }
            creature.changeCreature(currentBackground);
            fade = true;
            currentTime = Time.time;
        }

        if (fade)
        {
            float t = ((Time.time-fadeTime)/1000)*0.5f;
            currentAlphaDown = Interpolation.Lerp(maxAlpha, minAlpha, t);
            currentAlphaUp = Interpolation.Lerp(minAlpha, maxAlpha, t);
            
            if (currentBackground == 1)
            {
                backgrounds[0].alpha = currentAlphaDown;
                backgrounds[1].alpha = currentAlphaUp;
                if (backgrounds[0].alpha < 0.1 && backgrounds[1].alpha > 0.9)
                {
                    backgrounds[0].alpha = 0;
                    backgrounds[1].alpha = 1;
                    fade = false;
                }
            }
            
            if (currentBackground == 2)
            {
                backgrounds[1].alpha = currentAlphaDown;
                backgrounds[2].alpha = currentAlphaUp;
                if (backgrounds[1].alpha < 0.1 && backgrounds[2].alpha > 0.9)
                {
                    backgrounds[1].alpha = 0;
                    backgrounds[2].alpha = 1;
                    fade = false;
                }
            }
            
            if (currentBackground == 0)
            {
                backgrounds[2].alpha = currentAlphaDown;
                backgrounds[0].alpha = currentAlphaUp;
                if (backgrounds[2].alpha < 0.1 && backgrounds[0].alpha > 0.9)
                {
                    backgrounds[2].alpha = 0;
                    backgrounds[0].alpha = 1;
                    fade = false;
                }
            }
        }
    }
    
    public static class Interpolation
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }

	
    public void SetHighScore(int pHighScore)
    {
        highScore = pHighScore;
    }
}