using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace game {
  public enum Sound { PLAYER_DEAD, THICK_ICE_BREAK, KEY_GET, LEVEL_COMPLETE, THIN_ICE_BREAK, BLOCK_MOVE, LEVEL_START, TELEPORT, TREASURE_GET, KEY_USE }

  public static class SoundManager {
    private static SoundEffect[] soundEffects;
    private static Song song;

    public static void LoadContent(ContentManager content) {
      soundEffects = new SoundEffect[10];
      soundEffects[(int)Sound.PLAYER_DEAD] = content.Load<SoundEffect>("sounds\\player_dead");
      soundEffects[(int)Sound.THICK_ICE_BREAK] = content.Load<SoundEffect>("sounds\\thick_ice_break");
      soundEffects[(int)Sound.KEY_GET] = content.Load<SoundEffect>("sounds\\key_get");
      soundEffects[(int)Sound.KEY_USE] = content.Load<SoundEffect>("sounds\\key_use");
      soundEffects[(int)Sound.LEVEL_COMPLETE] = content.Load<SoundEffect>("sounds\\level_complete");
      soundEffects[(int)Sound.THIN_ICE_BREAK] = content.Load<SoundEffect>("sounds\\thin_ice_break");
      soundEffects[(int)Sound.BLOCK_MOVE] = content.Load<SoundEffect>("sounds\\block_move");
      soundEffects[(int)Sound.LEVEL_START] = content.Load<SoundEffect>("sounds\\level_start");
      soundEffects[(int)Sound.TELEPORT] = content.Load<SoundEffect>("sounds\\teleport");
      soundEffects[(int)Sound.TREASURE_GET] = content.Load<SoundEffect>("sounds\\treasure_get");

      song = content.Load<Song>("sounds\\music");
      MediaPlayer.IsRepeating = true;
      MediaPlayer.Volume = 0.5f;
      MediaPlayer.Play(song);
    }

    public static void Play(Sound sound, float volume = 1f) {
      soundEffects[(int)sound].Play(volume, 0f, 0f);
    }
  }
}