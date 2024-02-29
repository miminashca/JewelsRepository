using System;                   // System contains a lot of default C# libraries 
using GXPEngine;

public class MyGame : Game
{

	private Level level;
	HUD hud;
	LifeCounter lifeCounter;

	private Sound themeSound;
	private SoundChannel themeMusicChannel;

	private Sound menuSound;
	Player player;
	// High score is stored here so it's not overwritten when the level resets. Default is -1 so the game does not reset again if the player gets no score.
	int highScore = -1;
    public MyGame() : base(1920, 1080, false, false)
	{
		targetFps = 60;
		ResetLevel();

		themeSound = new Sound("sounds/theme.mp3", true);
		themeMusicChannel = themeSound.Play(false, 0U, 0.5f);

		menuSound = new Sound("sounds/menu.wav");
	}
	
	
	// void ChangeVolume() {
	// 	// If the UP or DOWN keys are pressed, we change the sound track volume (between 0 and 1):
	// 	if (Input.GetKey(Key.UP)) {
	// 		soundTrack.Volume += 0.01f;
	// 	}
	// 	if (Input.GetKey(Key.DOWN)) {
	// 		soundTrack.Volume -= 0.01f;
	// 	}
	// 	// Mathf contains various useful math functions.
	// 	// Clamp makes sure that a value stays within a given range: 0-1 here.
	// 	soundTrack.Volume = Mathf.Clamp(soundTrack.Volume, 0, 1);
	// }
	
	void Update() {
        // Instantly game over if highScore is the default value to give the illusion of a start screen.
        if (highScore < 0)
        {
            lifeCounter.ChangeLivesAmount(-3);
            highScore = 0;
        }

        if (level != null && Input.AnyKeyDown() && lifeCounter.gameOver)
		{
			menuSound.Play();
			level.cleanLevel();
			ResetLevel();
			Console.WriteLine("reset");
		}
	}

	//Main is the first method that's called when the program is run
	static void Main() {
		System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
		customCulture.NumberFormat.NumberDecimalSeparator = ".";
		System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
		// Create a "MyGame" and start it:
		new MyGame().Start();
	}
	
	void ResetLevel()
	{
		if (level != null)
		{
			level.Destroy();
			hud.Destroy();
			level = null;
		}
		// if (hud != null)
		// {
		// 	hud.Destroy();
		// 	hud = null;
		// }
		//
		// if (screen != null)
		// {
		// 	screen.Destroy();
		// 	screen = null;
		// }
		//
		// if (background != null)
		// {
		// 	background.Destroyed = true;
		// 	background.Destroy();
		// 	background = null;
		// 	
		// }

		// if (levelCount == 1)
		// {
		// 	level = new Level("levels/level_1.tmx");
		// 	level.musicChannel1 = level.level1Music.Play();
		// 	level.musicChannel1.Volume = 0.2f;
		// 	AddChildAt(level, 1);
		// }

		//UISound.Play();
		
		level = new Level("level_lab.tmx");
		AddChild(level);
		level.SetHighScore(highScore);
        hud = new HUD("level_lab.tmx");
        AddChild(hud);
		player = FindObjectOfType<Player>();
        // Doing this so the game has access to the game over variable and can reset the game.
        lifeCounter = FindObjectOfType<LifeCounter>();
        hud.SetObjects(player, lifeCounter, highScore);
    }
}