using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Pickable : GameObject {
    public Pickable(Texture2D texture) : base(texture) { }
    public Pickable(SpriteSheet spriteSheet) : base(spriteSheet) { }
    public virtual void OnPick(Level level) { }
  }
}
