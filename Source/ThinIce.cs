using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game {
  public class ThinIce : Game {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    
    public ThinIce() {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      ScreenManager.Initialize();
      Window.Title = "Thin Ice!";
      IsFixedTimeStep = true;
      TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 60);
      graphics.SynchronizeWithVerticalRetrace = false;
      graphics.PreferredBackBufferWidth = (int)Screen.width;
      graphics.PreferredBackBufferHeight = (int)Screen.height;
      graphics.ApplyChanges();
      base.Initialize();
    }

    protected override void LoadContent() {
      ScreenManager.LoadContent(Content);
      SoundManager.LoadContent(Content);
      spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime) {
      if (shouldQuit) Exit();
      ScreenManager.Update(gameTime);
      InputManager.Update();
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);
      spriteBatch.Begin();
      ScreenManager.Draw(spriteBatch);
      spriteBatch.End();
      base.Draw(gameTime);
    }

    public static readonly Random random = new();
    private static bool shouldQuit = false;
    public static void Quit() {
      shouldQuit = true;
    }
  }

  public static class Program {
    public static void Main() {
      using var game = new ThinIce();
      game.Run();
    }
  }
}