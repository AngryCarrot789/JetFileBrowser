using System.Threading.Tasks;
using JetFileBrowser.Actions.Contexts;
using JetFileBrowser.Shortcuts.Managing;

namespace JetFileBrowser.Shortcuts.Events {
    public delegate Task<bool> ShortcutActivatedEventHandler(ShortcutInputManager inputManager, GroupedShortcut shortcut, IDataContext context);
}