using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Gem : Canvas
{
	private bool collided = false;
	public bool collidedBox = false;
	private int gemPosType;
	private Random randomizer;

	private float gemSpawnPointY;
	private float gemSpawnPoint;
	private float gemSpawnDistance;
	private float gemSpeed;
	
	private Vector2 velocity;
	
	private float gravity = 0.6f;
	private float angle;
	private float angleSpeedY = 18;
	private float angleSpeedX = 0;
	private bool moves = false;
	private int currentTime;
	private int gemType;
	private int timeRangeSpeed = 5;

	private RotationReader rotationReader;
	private Player player;
	private Box box;
	private Level level;
	
	public Sound BoxSound;
	public Sound PlatformSound;
	public SoundChannel musicChannelBox;
	public SoundChannel musicChannelPlatform;
	
	private Random randomGemVol;
	private float gemVol;

	private AnimationSprite gemSprite;
	public Gem(RotationReader pRotationReader, Player pPlayer, Box pBox, Level pLevel) : base(100,100)
	{
		rotationReader = pRotationReader;
		//scale = 0.5f;
		
		velocity = new Vector2();
		
		gemSpawnPointY = height / 2;
		gemSpawnPoint = game.width / 2;
		gemSpawnDistance = 250;
		gemSpeed = 9;
			
		randomizer = new Random();
		gemType = randomizer.Next(0, 4);
		gemPosType = randomizer.Next(1,4);
		SetOrigin(width/2, height/2);

		y = gemSpawnPointY;
		switch (gemPosType)
		{
			case 1: x = gemSpawnPoint -30;
				break;
			case 2: x = gemSpawnPoint + gemSpawnDistance-30;
				break;
			case 3: x = gemSpawnPoint - gemSpawnDistance-30;
				break;
		}
		switch (gemType)
		{
			case 0: gemSprite = new AnimationSprite("sprites/gem1.png", 8, 1);
				break;
			case 1: gemSprite = new AnimationSprite("sprites/gem2.png", 8, 1);
				break;
			case 2: gemSprite = new AnimationSprite("sprites/gem3.png", 8, 1);
				break;
			case 3: gemSprite = new AnimationSprite("sprites/gem4.png", 8, 1);
				break;
		}
		
		collider.isTrigger = true;

		player = pPlayer;
		box = pBox;
		level = pLevel;
		
		BoxSound = new Sound("sounds/box.wav");
		PlatformSound = new Sound("sounds/platform.wav");
		
		randomGemVol = new Random();
		gemVol = randomGemVol.Next(2, 6);

		gemSprite.scale = 1.5f;
		AddChild(gemSprite);
		gemSprite.SetCycle(0, 8);
	}

	void Update()
	{
		if (rotation > 0){ Move(angleSpeedX, 0); }
		else { Move(-angleSpeedX, 0); }
        gemSprite.Animate(0.3f);
		if (!moves)
		{
            checkCollision();
            // moves = true;
            // currentTime = Time.time;
            y += gemSpeed;
		}
		else
		{
			angleMovement();
		}

		
		if (Time.time >= 1000 * timeRangeSpeed)
		{
			if (level.gemSpawnTime >= 0.2f)
			{
				level.gemSpawnTime -= 0.02f;
			}

			if (gemSpeed < 20)
			{
				gemSpeed += 0.2f;
			}
			timeRangeSpeed += timeRangeSpeed;
		}
		
	}

	private void angleMovement()
	{
		velocity.y += gravity;
		if (MoveUntilCollision(0, velocity.y) != null)
		{
			//velocity.y = 0;
			moves = false;
		}

		//x += angleSpeedX * (float)Math.Sin(angle);
        //Console.WriteLine("Launching at angle: {0}", (float)Math.Sin(angle));
        if (y == 600)
		{
            moves = false;
		}
	}

	private void checkCollision()
	{
		GameObject[] collissions = GetCollisions();
		foreach (GameObject collObj in collissions)
		{
			if (collObj is Controller && !moves)
			{
                angleSpeedX = 0;
                //Console.WriteLine("Colliding with platform.");
                musicChannelPlatform = PlatformSound.Play(false, 0U, gemVol / 10);
				rotation = rotationReader.controllerRotation;
				//Console.WriteLine("Launching at angle: {0}", angle);
				moves = true;
				velocity.y = -angleSpeedY;
				angleSpeedX += 10.5f;

				// if (gemType == 4 && !collided)
				// {
				// 	player.addScore(-100);
				// 	collided = true;
				// }
			}

			if (collObj is Box box)
			{
				musicChannelBox = BoxSound.Play(false, 0U, gemVol / 10);
				int boxType;
				boxType = box.getBoxType();
				if (boxType == gemType && !collided)
				{
					player.addScore(100);
					collided = true;
					//here
					collidedBox = true;
				}
			}
		}
	}

	public int getGemType()
	{
		return gemType;
	}
	
}

