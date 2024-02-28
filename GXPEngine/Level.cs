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
    private RotationReader rotationReader;
    
    private ArduinoControls arduinoControls;
    private LifeCounter lifeCounter;
    private PressButtonText pressButtonText;
    int currentTextTime;
    int textBlinkTime = 1250;
	
    private int currentGemTime;
    private ArrayList gems;
	
    private int gemDestructionPoint;
    private int gemSpawnTime;
	
    private Controller controller;
    public Level(string mapName)
    {
        gemDestructionPoint = 600;
        gemSpawnTime = 3;
		
        gems = new ArrayList();
        currentGemTime = Time.time;
        currentTextTime = Time.time;
        TiledLoader loader = new TiledLoader(mapName);
        loader.rootObject = this;
        loader.LoadTileLayers(0);
        loader.autoInstance = true;
        // I moved the object layer second so it displays below the HUD layer
        loader.LoadObjectGroups(0);
        player = FindObjectOfType<Player>();
        // Loading the HUD Layer seperately so it doesn't have any collisions, although addColliders doesn't seem to work for some reason, so I had to do it manually
        loader.addColliders = false;
        loader.LoadObjectGroups(1);
        pressButtonText = FindObjectOfType<PressButtonText>();

        controller = new Controller(player);
        AddChild(controller);
		
        rotationReader = new RotationReader(controller);
        AddChild(rotationReader);

        arduinoControls = new ArduinoControls();
        AddChild(arduinoControls);

        lifeCounter = new LifeCounter();
        AddChild(lifeCounter);
    }

    public void Update()
    {
        // Stopping action after the player has lost
        if (!lifeCounter.gameOver)
        {
            arduinoControls.UseFile(controller);

            if (Time.time - currentGemTime >= gemSpawnTime * 1000 && levelCleaned == false)
            {
                gem = new Gem(rotationReader);
                //AddChild(gem);
                gems.Add(gem);
                currentGemTime = Time.time;
            }

            foreach (Gem gem in gems.ToArray())
            {
                AddChild(gem);
                if (gem.y >= gemDestructionPoint)
                {
                    gems.Remove(gem);
                    gem.Destroy();
                    lifeCounter.ChangeLivesAmount(-1);
                }
            }
        }
        else if (!levelCleaned) { 
            // Cleaning the level once after the player has failed. 
            cleanLevel();
        }
        else if (Time.time - currentTextTime >= textBlinkTime)
        {
            // Blink text at a specific interval before starting a new run
            pressButtonText.visible = !pressButtonText.visible;
            currentTextTime = Time.time;
        }
    }

    public void cleanLevel()
    {
        foreach (Gem gem in gems.ToArray())
        {
              gems.Remove(gem);
              gem.Destroy();
              levelCleaned = true;
        }
    }
	
}