using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;

public class Gem : Sprite
{
	private int gemPosType;
	private Random randomizer;

	private float gemSpawnPointY;
	private float gemSpawnPoint;
	private float gemSpawnDistance;
	private float gemSpeed;
	
	private Vector2 velocity;
	
	private float gravity = .2f;
	private float angle;
	private float angleSpeedY = 9;
	private float angleSpeedX = 4.5f;
	private bool moves = false;
	private int currentTime;

	private RotationReader rotationReader;
	public Gem(RotationReader pRotationReader) : base("sprites/square.png")
	{
		rotationReader = pRotationReader;
		scale = 0.5f;
		
		velocity = new Vector2();
		
		gemSpawnPointY = height / 2;
		gemSpawnPoint = 300;
		gemSpawnDistance = 50;
		gemSpeed = 3;
			
		randomizer = new Random();
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
		
		collider.isTrigger = true;
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
			}
		}
	}
}

