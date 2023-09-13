using System.Threading.Tasks;
using JetFileBrowser.Actions;
using JetFileBrowser.History.ViewModels;
using JetFileBrowser.Notifications.Types;

namespace JetFileBrowser.History.Actions {
    public class RedoAction : AnAction {
        public override async Task<bool> ExecuteAsync(AnActionEventArgs e) {
            HistoryManagerViewModel manager = HistoryManagerViewModel.Instance;
            if (manager.HasRedoActions) {
                await manager.RedoAction();
            }
            else if (manager.NotificationPanel != null) {
                manager.NotificationPanel.PushNotification(new MessageNotification("Cannot redo", "There is nothing to redo!"));
            }

            return true;
        }
    }
}