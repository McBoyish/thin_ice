using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class FloorTile : GameObject {
    public FloorTile(Texture2D texture) : base(texture) { }
    public FloorTile(SpriteSheet spriteSheet) : base(spriteSheet) { }
    public virtual void OnPlayerLeave(Level level) { }
    public virtual void OnPlayerEnter(Level level) { }
    public virtual void OnBlockEnter(Block block) { }
  }
}
