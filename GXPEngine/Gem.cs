using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Gem : AnimationSprite
{
	private bool collided = false;
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
	private float angleSpeedX = 4.5f;
	private bool moves = false;
	private int currentTime;
	private int gemType;
	private int timeRangeSpeed = 5;

	private RotationReader rotationReader;
	private Player player;
	private Box box;
	private Level level;
	public Gem(RotationReader pRotationReader, Player pPlayer, Box pBox, Level pLevel) : base("sprites/square.png", 1, 1)
	{
		rotationReader = pRotationReader;
		scale = 0.5f;
		
		velocity = new Vector2();
		
		gemSpawnPointY = height / 2;
		gemSpawnPoint = 300;
		gemSpawnDistance = 50;
		gemSpeed = 3;
			
		randomizer = new Random();
		gemType = randomizer.Next(0, 5);
		gemPosType = randomizer.Next(1,4);
		SetOrigin(width/2, height/2);

		y = gemSpawnPointY;
		switch (gemPosType)
		{
			case 1: x = gemSpawnPoint;
				break;
			case 2: x = gemSpawnPoint + gemSpawnDistance;
				break;
			case 3: x = gemSpawnPoint + gemSpawnDistance*2;
				break;
		}
		switch (gemType)
		{
			case 0: color = 0xFF00FF;
				break;
			case 1: color = 0xFF0000;
				break;
			case 2: color = 0x00FF00;
				break;
			case 3: color = 0x0000FF;
				break;
			case 4: color = 0xFFFFFF;
				break;
		}
		
		collider.isTrigger = true;

		player = pPlayer;
		box = pBox;
		level = pLevel;
	}

	void Update()
	{
		checkCollision();
		if (!moves)
		{
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
			velocity.y = 0;
			moves = false;
		}
		
		x += angleSpeedX * (float)Math.Sin(angle);
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
			if (collObj is Controller pController)
			{
				angle = rotationReader.controllerRotation;
				moves = true;
				velocity.y = -angleSpeedY;
				if (gemType == 4 && !collided)
				{
					player.addScore(-100);
					collided = true;
				}
			}

			if (collObj is Box box)
			{
				int boxType;
				boxType = box.getBoxType();
				if (boxType == gemType && !collided)
				{
					player.addScore(100);
					collided = true;
				}
				
			}
		}
	}

	public int getGemType()
	{
		return gemType;
	}
	
	// protected override AnimationSprite createAnimationSprite()
	// {
	//     EasyDraw baseShape = new EasyDraw(30,60);
	//     baseShape.SetXY(0,-40);
	//     baseShape.Clear(ColorTranslator.FromHtml("#55FF0000"));
	//     AddChild(baseShape);
	//     return new AnimationSprite("baseShape", 1,1);
	// }
}

