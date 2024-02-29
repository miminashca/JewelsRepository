using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class HUD : GameObject
{
    EasyDraw UI;
    // Creating a seperate canvas for the timer so it can be changed seperately
    EasyDraw scoreUI;
    // Creating seperate canvas's for text that needs to appear seperately.
    EasyDraw highScoreText;
    EasyDraw lowScoreText;
    EasyDraw currentScoreText;

    Player player;
    LifeCounter lifeCounter;
    // Starting at -1 so the score is shown as 0 at the start.
    int previousPlayerScore = -1;
    int highScore;

    // Storing the coordinates as variable because I'm lazy
    float scoreX;
    float scoreY;
    Vector2 highScorePosition;
    float highScoreWidth;
    Vector2 lowScorePosition;
    float lowScoreWidth;
    Vector2 currentScorePosition;
    float currentScoreWidth;

    string filename;
    bool textDisplayed = false;
    public HUD(String pFilename)
    {
        filename = pFilename;
        highScorePosition = new Vector2();
        lowScorePosition = new Vector2();
        currentScorePosition = new Vector2();
        // Instantiating and setting up canvas properties
        UI = new EasyDraw(game.width, game.height, false);
        scoreUI = new EasyDraw(game.width, game.height, false);
        highScoreText = new EasyDraw(game.width, game.height, false);
        lowScoreText = new EasyDraw(game.width, game.height, false);
        currentScoreText = new EasyDraw(game.width, game.height, false);
        SetCanvasProperties(scoreUI, 46);
        SetCanvasProperties(UI, 46);
        SetCanvasProperties(highScoreText, 72);
        SetCanvasProperties(lowScoreText, 72);
        SetCanvasProperties(currentScoreText, 72);
        AddChild(scoreUI);

        Map levelData = MapParser.ReadMap(pFilename);
        SpawnUI(levelData);
    }

    void SpawnUI(Map level)
    {
        if (level.ObjectGroups == null)
        {
            Console.WriteLine("No object groups found.");
            return;
        }
        ObjectGroup objectGroup = level.ObjectGroups[3];
        if (objectGroup.Objects == null)
        {
            Console.WriteLine("Object group is empty.");
            return;
        }

        foreach (TiledObject obj in objectGroup.Objects)
        {
            // Acquiring text with names to be stored for later use.
            if (obj.Name != null)
            {
                if (obj.Name == "High Score")
                {
                    highScoreText.Text(obj.textField.text, obj.X, obj.Y);
                    highScorePosition.x = obj.X;
                    highScorePosition.y = obj.Y;
                    highScoreWidth = obj.Width;
                }
                if (obj.Name == "Low Score")
                {
                    lowScoreText.Text(obj.textField.text, obj.X, obj.Y);
                    lowScorePosition.x = obj.X;
                    lowScorePosition.y = obj.Y;
                    lowScoreWidth = obj.Width;
                }
                if (obj.Name == "Current Score")
                {
                    currentScoreText.Text(obj.textField.text, obj.X, obj.Y);
                    currentScorePosition.x = obj.X;
                    currentScorePosition.y = obj.Y;
                    currentScoreWidth = obj.Width;
                }
                if (obj.Name == "Score")
                {
                    scoreX = obj.X;
                    scoreY = obj.Y;
                }

            }
            else {
                //UI.Text(obj.textField.text, obj.X, obj.Y);
            }
        }
        AddChild(UI);
    }

    void SetCanvasProperties(EasyDraw canvas, int size)
    {
        canvas.TextFont("Upheaval TT (BRK)", 64);
        canvas.Fill(255);
        canvas.TextSize(size);
        canvas.TextAlign(CenterMode.Min, CenterMode.Min);
    }
    // Setting objects so their properties can be used in the class
    public void SetObjects(Player pPlayer, LifeCounter pLifeCounter, int pHighScore)
    {
        player = pPlayer;
        lifeCounter = pLifeCounter;
        highScore = pHighScore;
    }

    void Update()
    {
        if (player != null)
        {
            // Updates the score.
            if (player.score != previousPlayerScore)
            {
                previousPlayerScore = player.score;
                scoreUI.ClearTransparent();
                scoreUI.Text(player.score.ToString(), scoreX, scoreY);
            }


            if (lifeCounter.gameOver && !textDisplayed && highScore >= 0)
            {
                // Adds extra text if player beats their high score
                if (player.score > highScore)
                {
                    highScoreText.Text(player.score.ToString(), highScorePosition.x + highScoreWidth, highScorePosition.y);
                    AddChild(highScoreText);
                    highScore = player.score;
                }
                else
                {
                    // Adds extra text if player doesn't beat their high score
                    lowScoreText.Text(highScore.ToString(), lowScorePosition.x + lowScoreWidth / 1.15f, lowScorePosition.y);
                    AddChild(lowScoreText);
                    currentScoreText.Text(player.score.ToString(), currentScorePosition.x + currentScoreWidth / 1.15f, currentScorePosition.y);
                    AddChild(currentScoreText);
                }
                textDisplayed = true;
            }
        }
    }
}
