using System.Windows.Input;

namespace JetFileBrowser.WPF.Utils {
    public static class KBUtils {
        public static bool AreModsPressed(ModifierKeys keys) {
            return (Keyboard.Modifiers & keys) == keys;
        }
    }
}