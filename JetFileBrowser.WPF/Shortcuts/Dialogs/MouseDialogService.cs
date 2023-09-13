using JetFileBrowser.Shortcuts.Dialogs;
using JetFileBrowser.Shortcuts.Inputs;

namespace JetFileBrowser.WPF.Shortcuts.Dialogs {
    [ServiceImplementation(typeof(IMouseDialogService))]
    public class MouseDialogService : IMouseDialogService {
        public MouseStroke? ShowGetMouseStrokeDialog() {
            MouseStrokeInputWindow window = new MouseStrokeInputWindow();
            if (window.ShowDialog() != true || window.Stroke.Equals(default)) {
                return null;
            }
            else {
                return window.Stroke;
            }
        }
    }
}