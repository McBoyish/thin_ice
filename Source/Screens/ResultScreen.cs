using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class ResultScreen : Screen {
    private SpriteFont font;
    private Text scoreText;
    private Background background;
    private Background puffle;
    private Button finishButton;
    private readonly int score;

    public ResultScreen(int score) {
      this.score = score;
    }

    public override void Initialize() { }

    public override void LoadContent(ContentManager c) {
      base.LoadContent(c);
      font = content.Load<SpriteFont>("font\\arcade_big");

      background = new(content, "images\\result_screen");
      puffle = new(content, "images\\puffle");

      var puffleSize  = puffle.Size;
      puffle.Position = new Vector2(width / 2 - puffleSize.X / 2, 0.5f * height - puffleSize.Y / 2);

      finishButton = new(content, "images\\finish_button", "images\\finish_button_hover",
        () => { ThinIce.Quit(); });

      var textColor = new Color(0, 83, 165);
      scoreText = new Text(font) { TextString = "SCORE: " + score, Color = textColor };
      var textSize = scoreText.Size;
      scoreText.Position = new Vector2(width / 2 - textSize.X / 2, 0.1f * height - textSize.Y / 2);

      var size = finishButton.Size;
      var buttonPos = new Vector2(width / 2 - size.X / 2, 0.9f * height - size.Y / 2);
      finishButton.Position = buttonPos;
    }

    public override void Draw(SpriteBatch spriteBatch) {
      background.Draw(spriteBatch);
      puffle.Draw(spriteBatch);
      finishButton.Draw(spriteBatch);
      scoreText.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      finishButton.Update(gameTime);
    }
  }
}
