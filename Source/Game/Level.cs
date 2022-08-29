using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace game {
  public class Level {
    private readonly LevelResource resource;
    private readonly Dictionary<Vector2, WallTile> wallTiles;
    private readonly Dictionary<Vector2, FloorTile> floorTiles;
    private readonly Dictionary<Vector2, Pickable> pickables;
    private readonly Teleporters teleporters;
    private Block block;
    private Player player;
    private event Action NextLevel;
    private event Action ResetLevel;
    private event Action<int> OnScoreIncreased;
    private event Action OnIceBroken;

    public int IceCount { get; private set; }
    public int ScoreCount { get; set; }

    public Level(LevelResource resource, Action nextLevel, Action resetLevel, Action<int> onScoreIncreased, Action onIceBroken, int level) {
      this.resource = resource;
      NextLevel += nextLevel;
      ResetLevel += resetLevel;
      OnScoreIncreased += onScoreIncreased;
      OnIceBroken += onIceBroken;
      wallTiles = new();
      floorTiles = new();
      pickables = new();
      teleporters = new();
      block = null;
      IceCount = 0;
      ScoreCount = 0;
      LoadLevel(level);
    }

    public void Draw(SpriteBatch spriteBatch) {
      foreach (var tile in wallTiles) {
        tile.Value.Draw(spriteBatch);
      }
      foreach (var tile in floorTiles) {
        tile.Value.Draw(spriteBatch);
      }
      foreach (var pickable in pickables) {
        pickable.Value.Draw(spriteBatch);
      }
      teleporters.Draw(spriteBatch);
      block?.Draw(spriteBatch);
      player.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime) {
      foreach (var tile in wallTiles) {
        tile.Value.Update(gameTime);
      }
      foreach (var tile in floorTiles) {
        tile.Value.Update(gameTime);
      }
      foreach (var pickable in pickables) {
        pickable.Value.Update(gameTime);
      }
      teleporters.Update(gameTime);
      block?.Update(gameTime);
      player.Update(gameTime);
      CheckPickUp();
      CheckNearWall();
      CheckTeleport();
      HandleBlockCollision();
      if (HandleWin() || HandleDeath()) return;
      HandlePlayerMovement();
    }

    public void AcquireKey(Vector2 pos) {
      player.AcquireKey();
      pickables.Remove(pos);
    }

    public void AcquireTreasure(Vector2 pos) {
      SoundManager.Play(Sound.TREASURE_GET);
      OnScoreIncreased(100);
      pickables.Remove(pos);
    }

    public void BreakIce(Vector2 pos) {
      SoundManager.Play(Sound.THIN_ICE_BREAK);
      OnScoreIncreased(1);
      OnIceBroken();
      floorTiles.Remove(pos);
      wallTiles.Add(pos, new Water(resource.waterSpriteSheet) { Position = pos });
    }

    public void BreakThickIce(Vector2 pos) {
      SoundManager.Play(Sound.THICK_ICE_BREAK);
      OnScoreIncreased(1);
      OnIceBroken();
      floorTiles.Remove(pos);
      floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
    }

    public void Win() {
      
      player.Win();
      NextLevel();
    }

    public void Lose(Vector2 playerPos) {
      BreakIce(playerPos);
      player.Die(ResetLevel);
    }

    public void TryUnlockKeyHole(Vector2 pos) {
      if (!player.HasKey) return;
      player.UseKey();
      wallTiles.Remove(pos);
      floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
    }

    public bool CanBlockMove(Vector2 direction) {
      var movePostion = block.Position + direction * Screen.tileSize;
      return !wallTiles.ContainsKey(movePostion);
    }

    private void HandleBlockCollision() {
      if (block == null) return;
      var blockPos = block.Position;
      if (floorTiles.TryGetValue(blockPos, out FloorTile tile)) {
        tile.OnBlockEnter(block);
      }
      var blockMovePos = block.MovePosition;
      if (wallTiles.ContainsKey(blockMovePos)) {
        block.Stop();
      }
    }

    private void HandlePlayerMovement() {
      if (player.IsMoving) return;
      HandleInput();
      if (player.IsAboutToMove) {
        bool willCollide = HandlePlayerCollision();
        if (!willCollide) {
          player.Move();
          HandlePlayerLeaveTile();
        }
      }
    }

    private void CheckTeleport() {
      teleporters.TryTeleportPlayer(player);
      if (block != null) {
        teleporters.TryTeleportBlock(block);
      }
    }

    private void CheckPickUp() {
      var playerPos = player.Position;
      if (pickables.TryGetValue(playerPos, out Pickable pickable)) {
        pickable.OnPick(this);
      }
    }

    private void CheckNearWall() {
      var playerPos = player.Position;
      var tileSize = Screen.tileSize;
      var up = playerPos + (new Vector2(0f, -1f) * tileSize);
      var left = playerPos + (new Vector2(-1f, 0f) * tileSize);
      var down = playerPos + (new Vector2(0f, 1f) * tileSize);
      var right = playerPos + (new Vector2(1f, 0f) * tileSize);
      if (wallTiles.TryGetValue(up, out WallTile upTile)) {
        upTile.OnPlayerNear(this);
      }
      if (wallTiles.TryGetValue(left, out WallTile leftTile)) {
        leftTile.OnPlayerNear(this);
      }
      if (wallTiles.TryGetValue(down, out WallTile downTile)) {
        downTile.OnPlayerNear(this);
      }
      if (wallTiles.TryGetValue(right, out WallTile rightTile)) {
        rightTile.OnPlayerNear(this);
      }
    }

    private void HandleInput() {
      var playerPos = player.Position;
      if (InputManager.HasPressed(Keys.W)) {
        player.PrepareMove(new Vector2(playerPos.X, playerPos.Y - Screen.tileSize));
      } else if (InputManager.HasPressed(Keys.A)) {
        player.PrepareMove(new Vector2(playerPos.X - Screen.tileSize, playerPos.Y));
      } else if (InputManager.HasPressed(Keys.S)) {
        player.PrepareMove(new Vector2(playerPos.X, playerPos.Y + Screen.tileSize));
      } else if (InputManager.HasPressed(Keys.D)) {
        player.PrepareMove(new Vector2(playerPos.X + Screen.tileSize, playerPos.Y));
      }
    }

    private void HandlePlayerLeaveTile() {
      var pos = player.Position;
      if (floorTiles.TryGetValue(pos, out FloorTile tile)) {
        tile.OnPlayerLeave(this);
      }
    }

    private bool HandlePlayerCollision() {
      var movePos = player.MovePosition;
      if (wallTiles.TryGetValue(movePos, out WallTile tile)) {
        return tile.OnCollision(this, player);
      }
      if (block != null && block.Position == movePos) {
        return block.OnCollision(this, player);
      }
      return false;
    }

    private bool HandleWin() {
      if (player.HasWon) return true;
      var pos = player.Position;
      if (floorTiles.TryGetValue(pos, out FloorTile tile)) {
        tile.OnPlayerEnter(this);
      }
      return player.HasWon;
    }

    private bool HandleDeath() {
      if (player.IsDead) return true;
      var playerPos = player.Position;
      // get all 4 direction
      var up = new Vector2(0f, -1f);
      var left = new Vector2(-1f, 0f);
      var down = new Vector2(0f, 1f);
      var right = new Vector2(1f, 0f);
      bool shouldTileBreak = IsWall(up) && IsWall(left) && IsWall(down) && IsWall(right) && IsIce(playerPos);
      if (shouldTileBreak) {
        Lose(playerPos);
      }
      return shouldTileBreak;
    }

    public bool IsIce(Vector2 position) {
      if (floorTiles.TryGetValue(position, out FloorTile tile)) {
        return tile.Is(typeof(Ice));
      }
      return false;
    }

    private bool IsWall(Vector2 direction) {
      var pos = player.Position + direction * Screen.tileSize;
      if (block == null) {
        return wallTiles.ContainsKey(pos);
      } else {
        return wallTiles.ContainsKey(pos) || (block.Position == pos && !CanBlockMove(direction));
      }
    }

    private void LoadLevel(int level) {
      var lines = System.IO.File.ReadAllLines("Content\\levels\\level" + level.ToString() + ".txt");
      int y = 0;
      int x = 0;
      foreach (var line in lines) {
        var chs = line.ToCharArray();
        foreach (var ch in chs) {
          LoadGameOject(ch, x, y);
          x++;
        }
        x = 0;
        y++;
      }
    }

    private void LoadGameOject(char ch, int x, int y) {
      var tileSize = Screen.tileSize;
      var pos = new Vector2(x * tileSize, y * tileSize);
      switch (ch) {
        case 'X':
          // outside game level, used for displaying level info
          floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
          break;
        case '.':
          floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
          IceCount++;
          break;
        case '=':
          floorTiles.Add(pos, new ThickIce(resource.thickIceTexture) { Position = pos });
          IceCount += 2;
          break;
        case '*':
          floorTiles.Add(pos, new PlayerEnd(resource.playerEndTexture) { Position = pos });
          break;
        case 'p':
          floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
          player = new Player(resource.playerSpriteSheet) {
            Position = pos,
            FPS = 10
          };
          IceCount++;
          break;
        case '#':
          wallTiles.Add(pos, new Filler(resource.fillerTexture) { Position = pos });
          break;
        case '0':
          wallTiles.Add(pos, new Wall(resource.wallTexture) { Position = pos });
          break;
        case 'k':
          floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
          pickables.Add(pos, new Key(resource.keySpriteSheet) { Position = pos });
          IceCount++;
          break;
        case 's':
          floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
          pickables.Add(pos, new Treasure(resource.treasureTexture) { Position = pos });
          IceCount++;
          break;
        case 'x':
          wallTiles.Add(pos, new KeyHole(resource.keyHoleTexture) { Position = pos });
          break;
        case 't':
          bool added = teleporters.Add(new Teleporter(resource.teleporterSpriteSheet) { Position = pos });
          // limit teleporters to 2
          if (!added) {
            floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
            IceCount++;
          }
          break;
        case '@':
          floorTiles.Add(pos, new BlockEnd(resource.blockEndTexture) { Position = pos });
          break;
        case '+':
          floorTiles.Add(pos, new Ice(resource.iceTexture) { Position = pos });
          block = new Block(resource.blockTexture) { Position = pos };
          IceCount++;
          break;
        case 'T':
          floorTiles.Add(pos, new ThickIce(resource.thickIceTexture) { Position = pos });
          pickables.Add(pos, new Key(resource.keySpriteSheet) { Position = pos });
          IceCount += 2;
          break;
        case '$':
          floorTiles.Add(pos, new ThickIce(resource.thickIceTexture) { Position = pos });
          pickables.Add(pos, new Treasure(resource.treasureTexture) { Position = pos });
          IceCount += 2;
          break;
        case '&':
          floorTiles.Add(pos, new ThickIce(resource.thickIceTexture) { Position = pos });
          block = new Block(resource.blockTexture) { Position = pos };
          IceCount += 2;
          break;
        case '?':
          floorTiles.Add(pos, new BlockEnd(resource.blockEndTexture) { Position = pos });
          pickables.Add(pos, new Key(resource.keySpriteSheet) { Position = pos });
          break;
        case 'S':
          floorTiles.Add(pos, new BlockEnd(resource.blockEndTexture) { Position = pos });
          pickables.Add(pos, new Treasure(resource.treasureTexture) { Position = pos });
          break;
      }
    }
  }
}
