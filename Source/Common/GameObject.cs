using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game {
  public class GameObject : Object {
    private static readonly int defaultFPS = 10;
    // game object can either be animated or static
    // depending on wether a texture or a spritesheet is passed in the constructor
    private readonly Texture2D texture;
    private readonly SpriteSheet spriteSheet;
    private Animation currentAnimation;
    private string lastAnimationName;
    private int currentIndex;
    private float timeElapsed;
    private int tempFPS;
    private bool isPlayingOnce;
    private event Action OnAnimationComplete = null;
    
    public int FPS { get; set; }

    public GameObject() : base() {
      texture = null;
      spriteSheet = null;
      currentAnimation = null;
      lastAnimationName = "";
      currentIndex = 0;
      timeElapsed = 0f;
      tempFPS = defaultFPS;
      OnAnimationComplete = null;
      isPlayingOnce = false;
      FPS = defaultFPS;
    }

    public GameObject(Texture2D texture) : this() {
      this.texture = texture;
      Size = texture.Bounds.Size.ToVector2();
    }

    public GameObject(SpriteSheet spriteSheet) : this() {
      this.spriteSheet = spriteSheet;
      currentAnimation = spriteSheet.GetDefaultAnimation();
      Size = currentAnimation.GetFrame(0).Size.ToVector2();
    }


    public override void Draw(SpriteBatch spriteBatch) {
      if (texture != null) {
        spriteBatch.Draw(texture, Position, Color);
      } else if (spriteSheet != null) {
        spriteBatch.Draw(spriteSheet.Texture, Position, currentAnimation.GetFrame(currentIndex), Color);
      }
    }

    public override void Update(GameTime gameTime) {
      if (spriteSheet == null) return;

      timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
      if (timeElapsed < 1000 / FPS) return;

      if (isPlayingOnce && (currentIndex + 1) % currentAnimation.Count == 0) {
        currentAnimation = spriteSheet.GetAnimation(lastAnimationName);
        lastAnimationName = "";
        isPlayingOnce = false;
        FPS = tempFPS;
        tempFPS = defaultFPS;
        if (OnAnimationComplete != null) {
          OnAnimationComplete.Invoke();
          OnAnimationComplete = null;
        }
      }

      currentIndex = (currentIndex + 1) % currentAnimation.Count;
      timeElapsed = 0f;
    }

    public void Play(string animationName) {
      if (spriteSheet == null) return;
      currentAnimation = spriteSheet.GetAnimation(animationName);
      currentIndex = 0;
      timeElapsed = 0f;
    }

    public void PlayOnce(string animationName, int framesPerSecond = 0, Action onAnimationComplete = null) {
      if (spriteSheet == null) return;
      if (isPlayingOnce) return;
      lastAnimationName = currentAnimation.Name;
      currentAnimation = spriteSheet.GetAnimation(animationName);
      currentIndex = 0;
      timeElapsed = 0f;
      isPlayingOnce = true;
      if (framesPerSecond > 0) {
        // storing the prev animation fps
        tempFPS = FPS;
        FPS = framesPerSecond;
      }
      if (onAnimationComplete != null) {
        OnAnimationComplete += onAnimationComplete;
      }
    }

    public bool Is(Type type) {
      return GetType().Equals(type);
    }
  }
}
