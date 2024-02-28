using System;                   // System contains a lot of default C# libraries 
using GXPEngine;

public class MyGame : Game
{

	private Level level;
	public MyGame() : base(704, 704, false, false)
	{
		targetFps = 60;
		ResetLevel();
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
		Console.WriteLine(currentFps);
		if (level != null && Input.GetKeyDown(Key.R))
		{
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

	}
}