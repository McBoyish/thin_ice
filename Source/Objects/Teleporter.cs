using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Teleporter : FloorTile {
    public bool IsActive;

    public Teleporter(SpriteSheet spriteSheet) : base(spriteSheet) {
      IsActive = true;
    }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      base.Update(gameTime);
    }

    public void SetInactive() {
      Play("inactive");
      IsActive = false;
    }
  }
}
