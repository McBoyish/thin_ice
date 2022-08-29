using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace game {
  public class Background : Object {
    private readonly Texture2D texture;

    public Background(ContentManager content, string texturePath) : base() {
      texture = content.Load<Texture2D>(texturePath);
      Size = new Vector2(texture.Width, texture.Height);
    }

    public override void Draw(SpriteBatch spriteBatch) {
      spriteBatch.Draw(texture, Position, Color);
    }

    public override void Update(GameTime gameTime) { }
  }
}
