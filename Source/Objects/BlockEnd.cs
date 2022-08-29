using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class BlockEnd : FloorTile {
    public BlockEnd(Texture2D texture) : base(texture) { }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      base.Update(gameTime);
    }

    public override void OnBlockEnter(Block block) {
      block.Stop();
    }
  }
}
