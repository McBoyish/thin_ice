using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text.Json;

namespace game {
  public class Frame {
    private readonly int x;
    private readonly int y;
    public readonly int w;
    public readonly int h;

    public Frame(int x, int y, int w, int h) {
      this.x = x;
      this.y = y;
      this.w = w;
      this.h = h;
    }

    public Rectangle ToRectangle() {
      return new Rectangle(x, y, w, h);
    }
  }

  public class Animation {
    private readonly List<Frame> frames;

    public int Count {
      get {
        return frames.Count;
      }
    }

    public string Name { get; private set; }

    public Animation(string name) {
      Name = name;
      frames = new();
    }

    public void AddFrame(Frame frame) {
      frames.Add(frame);
    }

    public Rectangle GetFrame(int index) {
      return frames[index].ToRectangle();
    }
  }

  public class SpriteSheet {
    private readonly Dictionary<string, Animation> animations;
    // first animation in json array is chosen as default
    private readonly string defaultAnimationName;

    public Texture2D Texture { get; private set; }

    public SpriteSheet(ContentManager content, string path) {
      var texturePath = path;
      var JSONPath = "Content\\" + path + ".json";

      animations = new();
      Texture = content.Load<Texture2D>(texturePath);
      string json = System.IO.File.ReadAllText(JSONPath);

      var options = new JsonDocumentOptions {
        AllowTrailingCommas = true
      };

      int count = 0;
      using JsonDocument document = JsonDocument.Parse(json, options);
      foreach (JsonElement animation in document.RootElement.EnumerateArray()) {
        var animationName = animation.GetProperty("name").GetString();
        var frames = animation.GetProperty("frames").EnumerateArray();

        if (count == 0) defaultAnimationName = animationName;
        animations.Add(animationName, new Animation(animationName));

        foreach (JsonElement frame_ in frames) {
          var x = frame_.GetProperty("x").GetInt32();
          var y = frame_.GetProperty("y").GetInt32();
          var w = frame_.GetProperty("w").GetInt32();
          var h = frame_.GetProperty("h").GetInt32();
          animations[animationName].AddFrame(new Frame(x, y, w, h));
        }

        count++;
      }
    }

    public Animation GetAnimation(string animationName) {
      return animations[animationName];
    }

    public Animation GetDefaultAnimation() {
      return animations[defaultAnimationName];
    }
  }
}
