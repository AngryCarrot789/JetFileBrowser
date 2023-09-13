using System.Threading.Tasks;
using JetFileBrowser.Actions;
using JetFileBrowser.History.ViewModels;
using JetFileBrowser.Notifications.Types;

namespace JetFileBrowser.History.Actions {
    public class UndoAction : AnAction {
        public override async Task<bool> ExecuteAsync(AnActionEventArgs e) {
            HistoryManagerViewModel manager = HistoryManagerViewModel.Instance;
            if (manager.HasUndoActions) {
                await manager.UndoAction();
            }
            else if (manager.NotificationPanel != null) {
                manager.NotificationPanel.PushNotification(new MessageNotification("Cannot undo", "There is nothing to undo!"));
            }

            return true;
        }
    }
}