using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class LevelResource {
    public readonly SpriteSheet playerSpriteSheet;
    public readonly SpriteSheet waterSpriteSheet;
    public readonly SpriteSheet keySpriteSheet;
    public readonly SpriteSheet teleporterSpriteSheet;
    public readonly Texture2D iceTexture;
    public readonly Texture2D thickIceTexture;
    public readonly Texture2D wallTexture;
    public readonly Texture2D waterTexture;
    public readonly Texture2D fillerTexture;
    public readonly Texture2D playerEndTexture;
    public readonly Texture2D keyHoleTexture;
    public readonly Texture2D treasureTexture;
    public readonly Texture2D blockTexture;
    public readonly Texture2D blockEndTexture;

    public LevelResource(ContentManager content) {
      playerSpriteSheet = new SpriteSheet(content, "images\\player");
      waterSpriteSheet = new SpriteSheet(content, "images\\water");
      keySpriteSheet = new SpriteSheet(content, "images\\key");
      teleporterSpriteSheet = new SpriteSheet(content, "images\\teleporter");
      iceTexture = content.Load<Texture2D>("images\\ice");
      thickIceTexture = content.Load<Texture2D>("images\\thick_ice");
      wallTexture = content.Load<Texture2D>("images\\wall");
      fillerTexture = content.Load<Texture2D>("images\\filler");
      playerEndTexture = content.Load<Texture2D>("images\\player_end");
      keyHoleTexture = content.Load<Texture2D>("images\\key_hole");
      treasureTexture = content.Load<Texture2D>("images\\treasure");
      blockTexture = content.Load<Texture2D>("images\\block");
      blockEndTexture = content.Load<Texture2D>("images\\block_end");
    }
  }
}
