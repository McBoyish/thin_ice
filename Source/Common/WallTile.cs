using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace game {
  public class WallTile : GameObject {
    public WallTile(Texture2D texture) : base(texture) { }
    public WallTile(SpriteSheet spriteSheet) : base(spriteSheet) { }

    public virtual bool OnCollision(Level level, Player player) {
      player.CancelMove();
      return true;
    }

    public virtual void OnPlayerNear(Level level) { }
  }
}
