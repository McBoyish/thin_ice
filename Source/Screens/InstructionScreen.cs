using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class InstructionScreen : Screen {
    private Background background;
    private Button playButton;

    public override void Initialize() { }

    public override void LoadContent(ContentManager c) {
      base.LoadContent(c);

      background = new Background(content, "images\\instruction_screen");

      playButton = new Button(content, "images\\play_button", "images\\play_button_hover", 
        () => { ScreenManager.AddScreen(new GameScreen()); });

      var size = playButton.Size;
      playButton.Position = new Vector2(width / 2 - size.X / 2, 0.9f * height - size.Y / 2);
    }

    public override void Draw(SpriteBatch spriteBatch) {
      background.Draw(spriteBatch);
      playButton.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      playButton.Update(gameTime);
    }
  }
}