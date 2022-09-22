using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game {
  public class GameScreen : Screen {
    private const int maxLevel = 19;
    private SpriteFont font;
    private LevelResource resource;

    private Text currentLevelText;
    private Text iceBrokenText;
    private Text solvedText;
    private Text scoreText;
    private Text resetText;

    private Level level;
    private int iceCount;
    private int iceBrokenCount;
    private int currentLevel;
    private int solvedCount;
    private int scoreCount;

    private int scoreDisplay;
    private int scoreBuffer;
    private const int scoreStep = 3;

    public int IceBrokenCount {
      get { return iceBrokenCount; }
      set {
        iceBrokenCount = value;
        iceBrokenText.TextString = GetIceBrokenText();
        iceBrokenText.Position = new Vector2(width / 2 - iceBrokenText.Size.X / 2, tileSize / 2 - iceBrokenText.Size.Y / 2);
      }
    }

    public int CurrentLevel {
      get { return currentLevel; }
      set {
        currentLevel = value;
        currentLevelText.TextString = GetLevelText();
        currentLevelText.Position = new Vector2(tileSize, tileSize / 2 - currentLevelText.Size.Y / 2);
      }
    }

    public int SolvedCount {
      get { return solvedCount; }
      set {
        solvedCount = value;
        solvedText.TextString = GetSolvedText();
        solvedText.Position = new Vector2(width - tileSize - solvedText.Size.X, tileSize / 2 - solvedText.Size.Y / 2);
      }
    }

    public int ScoreDisplay {
      get { return scoreDisplay; }
      set {
        scoreDisplay = value;
        scoreText.TextString = GetScoreText();
        scoreText.Position = new Vector2(width - tileSize - solvedText.Size.X, height - tileSize + (tileSize / 2 - scoreText.Size.Y / 2));
      }
    }

    public int ScoreCount {
      get { return scoreCount; }
      set {
        scoreCount = value;
        scoreText.TextString = GetScoreText();
        scoreText.Position = new Vector2(width - tileSize - solvedText.Size.X, height - tileSize + (tileSize / 2 - scoreText.Size.Y / 2));
      }
    }

    public override void Initialize() {
      currentLevel = 5;
      solvedCount = 0;
      iceBrokenCount = 0;
      scoreCount = 0;
      scoreDisplay = 0;
      scoreBuffer = 0;
    }

    public override void LoadContent(ContentManager c) {
      base.LoadContent(c);
      font = content.Load<SpriteFont>("font\\arcade");

      resource = new LevelResource(content);
      level = new Level(resource, NextLevel, ResetLevel, OnScoreIncreased, OnIceBroken, currentLevel);
      iceCount = level.IceCount;

      var textColor = new Color(0, 83, 165);
      currentLevelText = new Text(font) { TextString = GetLevelText(), Color = textColor };
      iceBrokenText = new Text(font) { TextString = GetIceBrokenText(), Color = textColor };
      solvedText = new Text(font) { TextString = GetSolvedText(), Color = textColor };
      scoreText = new Text(font) { TextString = GetScoreText(), Color = textColor };
      resetText = new Text(font, ResetLevelWithSound, Color.Red) { TextString = "RESET", Color = textColor };
      SetTextPosition();

      SoundManager.Play(Sound.LEVEL_START);
    }

    public override void Draw(SpriteBatch spriteBatch) {
      level.Draw(spriteBatch);
      currentLevelText.Draw(spriteBatch);
      iceBrokenText.Draw(spriteBatch);
      solvedText.Draw(spriteBatch);
      resetText.Draw(spriteBatch);
      scoreText.Draw(spriteBatch);

      if (scoreBuffer > 0) {
        var step = Math.Min(scoreStep, scoreBuffer);
        ScoreDisplay += step;
        scoreBuffer -= step;
      }
    }

    public override void Update(GameTime gameTime) {
      level.Update(gameTime);
      resetText.Update(gameTime);
    }

    private void OnScoreIncreased(int amount) {
      ScoreCount += amount;
      level.ScoreCount += amount;
      scoreBuffer += amount;
    }

    private void OnIceBroken() {
      IceBrokenCount++;
    }

    private void ResetLevelWithSound() {
      SoundManager.Play(Sound.LEVEL_START);
      ResetLevel();
    }

    private void ResetLevel() {
      ScoreCount -= level.ScoreCount;
      ScoreDisplay = ScoreCount;
      GenerateLevel(currentLevel);
    }

    private void NextLevel() {
      CurrentLevel++;
      SolvedCount++;
      OnScoreIncreased(100);
      if (CurrentLevel > maxLevel) {
        ScreenManager.AddScreen(new ResultScreen(ScoreCount));
      } else {
        GenerateLevel(CurrentLevel);
      }
    }

    private void GenerateLevel(int n) {
      level = new Level(resource, NextLevel, ResetLevel, OnScoreIncreased, OnIceBroken, n);
      iceCount = level.IceCount;
      IceBrokenCount = 0;
    }

    private void SetTextPosition() {
      var tileSize = Screen.tileSize;
      currentLevelText.Position = new Vector2(tileSize, tileSize / 2 - currentLevelText.Size.Y / 2);
      iceBrokenText.Position = new Vector2(width / 2 - iceBrokenText.Size.X / 2, tileSize / 2 - iceBrokenText.Size.Y / 2);
      solvedText.Position = new Vector2(width - tileSize - solvedText.Size.X, tileSize / 2 - solvedText.Size.Y / 2);
      resetText.Position = new Vector2(tileSize, height - tileSize + (tileSize / 2 - resetText.Size.Y / 2));
      scoreText.Position = new Vector2(width - tileSize - solvedText.Size.X, height - tileSize + (tileSize / 2 - scoreText.Size.Y / 2));
    }

    private string GetLevelText() {
      return "LEVEL " + CurrentLevel;
    }

    private string GetIceBrokenText() {
      return IceBrokenCount + "/" + iceCount;
    }

    private string GetSolvedText() {
      return "SOLVED " + SolvedCount;
    }

    private string GetScoreText() {
      return "SCORE " + scoreDisplay;
    }
  }
}