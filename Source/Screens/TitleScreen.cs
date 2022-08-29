using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class TitleScreen : Screen {
    private Background background;
    private Button startButton;

    public override void Initialize() { }

    public override void LoadContent(ContentManager c) {
      base.LoadContent(c);
      background = new(content, "images\\title_screen");

      startButton = new(content, "images\\start_button", "images\\start_button_hover", 
        () => { ScreenManager.AddScreen(new InstructionScreen()); });

      var size = startButton.Size;
      var buttonPos = new Vector2(width / 2 - size.X / 2, 0.9f * height - size.Y / 2);
      startButton.Position = buttonPos;
    }

    public override void Draw(SpriteBatch spriteBatch) {
      background.Draw(spriteBatch);
      startButton.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      startButton.Update(gameTime);
    }
  }
}
