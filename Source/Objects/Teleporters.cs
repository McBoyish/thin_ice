using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Teleporters {
    public Teleporter T1 { get; set; }
    public Teleporter T2 { get; set; }

    public Teleporters() {
      T1 = null;
      T2 = null;
    }

    public void Draw(SpriteBatch spriteBatch) {
      if (!HasTeleporters()) return;
      T1.Draw(spriteBatch);
      T2.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime) {
      if (!HasTeleporters()) return;
      T1.Update(gameTime);
      T2.Update(gameTime);
    }

    public bool Add(Teleporter t) {
      if (T1 == null) {
        T1 = t;
        return true;
      } 
      if (T2 == null) {
        T2 = t;
        return true;
      }
      return false;
    }

    public void TryTeleportPlayer(Player player) {
      if (!HasTeleporters() || !CanTeleport()) return;

      var playerPos = player.Position;

      if (playerPos == T1.Position) {
        SoundManager.Play(Sound.TELEPORT);
        player.Position = T2.Position;
        DisableTeleporters();
        return;
      }

      if (playerPos == T2.Position) {
        SoundManager.Play(Sound.TELEPORT);
        player.Position = T1.Position;
        DisableTeleporters();
        return;
      }
    }

    public bool Contain(Vector2 pos) {
      return T1.Position == pos || T2.Position == pos;
    }

    public void TryTeleportBlock(Block block) {
      if (!HasTeleporters() || !CanTeleport()) return;

      var blockPos = block.Position;

      if (blockPos == T1.Position) {
        block.Teleport(T2.Position);
        return;
      }

      if (blockPos == T2.Position) {
        block.Teleport(T1.Position);
        return;
      }
    }

    private bool HasTeleporters() {
      return (T1 != null && T2 != null);
    }

    private bool CanTeleport() {
      return (T1.IsActive && T2.IsActive);
    }

    private void DisableTeleporters() {
      T1.SetInactive();
      T2.SetInactive();
    }
  }
}
