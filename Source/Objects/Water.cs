using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Water : WallTile {
   public Water(SpriteSheet spriteSheet) : base(spriteSheet) {
      FPS = 30;
      PlayOnce("ice break", 20);
    }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      base.Update(gameTime);
    }
  }
}
