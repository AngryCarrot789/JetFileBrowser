using System.Threading.Tasks;
using JetFileBrowser.Utils;

namespace JetFileBrowser.Actions {
    public class RenameAction : AnAction {
        public override async Task<bool> ExecuteAsync(AnActionEventArgs e) {
            if (!e.DataContext.TryGetContext(out IRenameTarget renameable))
                return false;
            await renameable.RenameAsync();
            return true;
        }
    }
}