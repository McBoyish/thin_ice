using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game {
  public class Text : Object {
    private readonly SpriteFont font;
    private string textString;
    private event Action OnClick;
    private Color onHoverColor;
    private bool isHovered;

    public string TextString {
      get { return textString; }
      set {
        textString = value;
        Size = font.MeasureString(value);
      }
    }

    public Text(SpriteFont spriteFont) : base() {
      font = spriteFont;
      TextString = "N/A";
      OnClick = null;
      isHovered = false;
      onHoverColor = Color.White;
    }

    public Text(SpriteFont spriteFont, Action onClick, Color onHoverColor) : base() {
      font = spriteFont;
      TextString = "N/A";
      OnClick += onClick;
      isHovered = false;
      this.onHoverColor = onHoverColor; 
    }

    public override void Draw(SpriteBatch spriteBatch) {
      spriteBatch.DrawString(font, TextString, Position, isHovered ? onHoverColor : Color);
    }

    public override void Update(GameTime gameTime) {
      if (OnClick == null) return;
      isHovered = InputManager.HasHoveredOver(Position, Size);
      if (InputManager.HasClickedOn(Position, Size)) {
        OnClick.Invoke();
      }
    }
  }
}
