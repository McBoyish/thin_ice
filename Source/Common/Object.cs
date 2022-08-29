using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Object {
    public Vector2 Position { get; set; }
    public Color Color { get; set; }
    public Vector2 Size { get; set; }

    public Object() {
      Position = Vector2.Zero;
      Size = Vector2.Zero;
      Color = Color.White;
    }

    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void Update(GameTime gameTime) { }
  }
}
