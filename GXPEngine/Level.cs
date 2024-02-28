using System;
using System.Collections;
using GXPEngine.Core;
using System.Drawing;
using TiledMapParser;
using GXPEngine;

public class Level : GameObject
{
    private Player player;
    private Gem gem;
    private RotationReader rotationReader;
    
    private ArduinoControls arduinoControls;
    private LifeCounter lifeCounter;
	
    private int currentTime;
    private ArrayList gems;
	
    private int gemDestructionPoint;
    private int gemSpawnTime;
	
    private Controller controller;
    public Level(string mapName)
    {
        gemDestructionPoint = 600;
        gemSpawnTime = 3;
		
        gems = new ArrayList();
        currentTime = Time.time;
        TiledLoader loader = new TiledLoader(mapName);
        loader.rootObject = this;
        loader.LoadTileLayers(0);
        loader.autoInstance = true;
        loader.LoadObjectGroups();
        player = FindObjectOfType<Player>();
        // Loading the HUD Layer seperately so it doesn't have any collisions
        //loader.addColliders = false;
        //loader.LoadObjectGroups(1);

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
        //these lines you added and after that I can no longer run the build. Without them everything works fine.
        arduinoControls.UseFile(controller);
		
        if (Time.time - currentTime >= gemSpawnTime*1000)
        {
            gem = new Gem(rotationReader);
            //AddChild(gem);
            gems.Add(gem);
            currentTime = Time.time;
        }

        foreach (Gem gem in gems.ToArray())
        {
            AddChild(gem);
            if (gem.y >= gemDestructionPoint)
            {
                gems.Remove(gem);
                gem.Destroy();
            }
        }
    }
	
}