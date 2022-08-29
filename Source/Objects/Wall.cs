using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Wall : WallTile {
    public Wall(Texture2D texture) : base(texture) { }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      base.Update(gameTime);
    }
  }
}
