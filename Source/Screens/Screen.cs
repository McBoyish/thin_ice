using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Screen {
    protected ContentManager content;

    public virtual void Initialize() { }

    public virtual void LoadContent(ContentManager content) {
      this.content = new ContentManager(content.ServiceProvider, "Content");
    }

    public virtual void UnloadContent() {
      content.Unload();
    }

    public virtual void Draw(SpriteBatch spriteBatch) { }

    public virtual void Update(GameTime gameTime) { }

    #region static
    public static readonly float width = 475f;
    public static readonly float height = 425f;
    public static readonly float tileSize = 25f;
    #endregion
  }
}
