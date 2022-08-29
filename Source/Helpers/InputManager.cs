using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game {
  public static class InputManager {
    private static MouseState lastMouseState = new();
    private static KeyboardState lastKeyboardState = new();

    public static void Update() {
      lastMouseState = Mouse.GetState();
      lastKeyboardState = Keyboard.GetState();
    }

    public static bool HasPressed(Keys key) {
      KeyboardState keyboardState = Keyboard.GetState();
      return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
    }

    public static bool HasClicked() {
      MouseState mouseState = Mouse.GetState();
      return lastMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed;
    }

    public static bool HasHoveredOver(Vector2 location, Vector2 size) {
      MouseState mouseState = Mouse.GetState();
      int x = mouseState.X;
      int y = mouseState.Y;
      return x >= location.X && x <= location.X + size.X && y >= location.Y && y <= location.Y + size.Y;
    }

    public static bool HasClickedOn(Vector2 location, Vector2 size) {
      return HasClicked() && HasHoveredOver(location, size);
    }
  }
}
