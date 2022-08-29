using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game {
  public class Button : Object {
    private readonly Texture2D textureDefault;
    private readonly Texture2D textureHover;
    private Texture2D currentTexture;
    private event Action OnPress;

    public Button(ContentManager content, string textureDefaultPath, string textureHoverPath, Action onPress) : base() {
      textureDefault = content.Load<Texture2D>(textureDefaultPath);
      textureHover = content.Load<Texture2D>(textureHoverPath);
      currentTexture = textureDefault;
      OnPress += onPress;
      Size = new Vector2(currentTexture.Width, currentTexture.Height);
    }

    public override void Draw(SpriteBatch spriteBatch) {
      spriteBatch.Draw(currentTexture, Position, Color);
    }

    public override void Update(GameTime gameTime) {
      currentTexture = textureDefault;
      if (InputManager.HasHoveredOver(Position, Size)) {
        currentTexture = textureHover;
      }
      if (InputManager.HasClickedOn(Position, Size)) {
        OnPress.Invoke();
      }
    }
  }
}
