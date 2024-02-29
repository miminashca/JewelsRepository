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
    
    //these lines you added and after that I can no longer run the build. Without them everything works fine.
    //private ArduinoControls arduinoControls;
	
    private int currentTime;
    private ArrayList gems;
	
    private int gemDestructionPoint;
    public float gemSpawnTime;
	
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
		
        controller = new Controller(player);
        AddChild(controller);
		
        rotationReader = new RotationReader(controller);
        AddChild(rotationReader);

        box = FindObjectOfType<Box>();

        //these lines you added and after that I can no longer run the build. Without them everything works fine.
        //arduinoControls = new ArduinoControls();
        //AddChild(arduinoControls);
    }

    public void Update()
    {
        //these lines you added and after that I can no longer run the build. Without them everything works fine.
        //arduinoControls.UseFile(controller);
		
        if (Time.time - currentTime >= gemSpawnTime*1000 && levelCleaned == false)
        {
            gem = new Gem(rotationReader, player, box, this);
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
            //GameObject[] collissions = GetCollisions();
            // foreach (GameObject collObj in collissions)
            // {
            //     if (collObj is Box box)
            //     {
            //         gems.Remove(gem);
            //         gem.Destroy();
            //     }
            // }
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