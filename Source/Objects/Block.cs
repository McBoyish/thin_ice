using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game {
  public class Block : WallTile {
    private readonly float speed;
    private float distanceMoved;

    public bool IsMoving { get; private set; }
    public Vector2 MovePosition { get; private set; }
    public Vector2 MoveDirection { get; private set; }

    public Block(Texture2D texture) : base(texture) {
      speed = 0.5f; // in pixels per ms
      distanceMoved = 0f;
      IsMoving = false;
      MoveDirection = Vector2.Zero;
      MovePosition = Vector2.Zero;
    }

    public override void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime) {
      base.Update(gameTime);
      if (IsMoving) {
        var elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
        HandleMovement(elapsedTime);
      }
    }

    public void Teleport(Vector2 pos) {
      SoundManager.Play(Sound.TELEPORT, 0.75f);
      Position = pos;
      MovePosition = pos + MoveDirection * Screen.tileSize;
    }

    public void Move(Vector2 moveDirection) {
      if (IsMoving) return;
      SoundManager.Play(Sound.BLOCK_MOVE);
      IsMoving = true;
      MoveDirection = moveDirection;
      MovePosition = Position + moveDirection * Screen.tileSize;
    }

    public void Stop() {
      if (!IsMoving) return;
      IsMoving = false;
      MoveDirection = Vector2.Zero;
      MovePosition = Vector2.Zero;
    }

    public override bool OnCollision(Level level, Player player) {
      if (level.CanBlockMove(player.MoveDirection)) {
        Move(player.MoveDirection);
        return false;
      } else {
        player.CancelMove();
        return true;
      }
    }

    private void HandleMovement(float elapsedTime) {
      var displacement = MoveDirection * speed * elapsedTime;
      Position += displacement;
      distanceMoved += displacement.Length();
      if (distanceMoved >= Screen.tileSize) {
        // ensure that movement is tile based
        Position = MovePosition;
        MovePosition = Position + MoveDirection * Screen.tileSize;
        distanceMoved = 0f;
      }
    }
  }
}