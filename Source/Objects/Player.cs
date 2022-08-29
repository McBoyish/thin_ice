using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game {
  public class Player : GameObject {
    private readonly float speed;
    private float distanceMoved;
    private float timeToNextBlink;
    private int keyCount;
    
    public bool HasKey { get { return keyCount > 0; } }
    public bool IsAboutToMove { get; private set; }
    public bool IsMoving { get; private set; }
    public Vector2 MovePosition { get; private set; }
    public Vector2 MoveDirection { get; private set; }
    public bool IsDead { get; private set; }
    public bool HasWon { get; private set; }

    public Player(SpriteSheet spriteSheet) : base(spriteSheet) {
      speed = 0.5f; // in pixels per ms
      distanceMoved = 0;
      timeToNextBlink = ThinIce.random.Next(3000, 5000);
      IsAboutToMove = false;
      IsMoving = false;
      MovePosition = Vector2.Zero;
      MoveDirection = Vector2.Zero;
      IsDead = false;
      HasWon = false;
    }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      base.Update(gameTime);
      var elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
      HandleBlink(elapsedTime);
      if (!IsDead && !HasWon && IsMoving) {
        HandleMovement(elapsedTime);
      }
    }

    public void AcquireKey() {
      SoundManager.Play(Sound.KEY_GET);
      keyCount++;
    }

    public void UseKey() {
      SoundManager.Play(Sound.KEY_USE);
      keyCount--;
    }

    public void PrepareMove(Vector2 pos) {
      IsAboutToMove = true;
      MovePosition = pos;
      var displacement = pos - Position;
      MoveDirection = displacement / displacement.Length();
    }

    public void CancelMove() {
      IsAboutToMove = false;
    }

    public void Move() {
      IsMoving = true;
      IsAboutToMove = false;
    }

    public void Die(Action onAnimationComplete) {
      SoundManager.Play(Sound.PLAYER_DEAD);
      IsDead = true;
      PlayOnce("death", 45, onAnimationComplete);
    }

    public void Win() {
      SoundManager.Play(Sound.LEVEL_COMPLETE);
      HasWon = true;
    }

    private void Blink() {
      PlayOnce("blink", 20);
    }

    private void HandleBlink(float elapsedTime) {
      timeToNextBlink -= elapsedTime;
      if (timeToNextBlink < 0) {
        Blink();
        timeToNextBlink = ThinIce.random.Next(3000, 5000);
      }
    }

    private void HandleMovement(float elapsedTime) {
      var displacement = MoveDirection * speed * elapsedTime;
      Position += displacement;
      distanceMoved += displacement.Length();
      if (distanceMoved >= Screen.tileSize) {
        IsMoving = false;
        Position = MovePosition;
        distanceMoved = 0;
      }
    }
  }
}
