namespace game {
  public static class Debug {
    public static void Assert(bool condition) {
      System.Diagnostics.Debug.Assert(condition);
    }

    public static void Write(string message) {
      System.Diagnostics.Debug.Write(message);
    }

    public static void WriteLine(string message) {
      System.Diagnostics.Debug.WriteLine(message);
    }
  }
}
