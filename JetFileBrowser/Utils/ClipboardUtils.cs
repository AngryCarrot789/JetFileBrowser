using System.Threading.Tasks;
using JetFileBrowser.Views.Dialogs.Message;

namespace JetFileBrowser.Utils {
    public static class ClipboardUtils {
        public static async Task<bool> SetClipboardOrShowErrorDialog(string text) {
            if (IoC.Clipboard == null) {
                await Dialogs.ClipboardUnavailableDialog.ShowAsync("No clipboard", "Clipboard is unavailable.\n" + text);
                return false;
            }
            else if (!IoC.Clipboard.SetText(text)) {
                await Dialogs.ClipboardUnavailableDialog.ShowAsync("Clipboard error", "Failed to set clipboard text to:\n" + text);
                return false;
            }
            else {
                return true;
            }
        }
    }
}