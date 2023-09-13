using System.Threading.Tasks;
using JetFileBrowser.Shortcuts.Managing;

namespace JetFileBrowser.WPF.Shortcuts {
    public delegate Task<bool> ShortcutActivateHandler(ShortcutInputManager inputManager, GroupedShortcut shortcut);
}