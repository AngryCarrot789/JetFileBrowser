using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JetFileBrowser.History {
    public class MultiHistoryAction : HistoryAction {
        public List<HistoryAction> Actions { get; }

        public MultiHistoryAction(List<HistoryAction> actions) {
            this.Actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }

        public override async Task UndoAsync() {
            foreach (HistoryAction action in this.Actions) {
                await action.UndoAsync();
            }
        }

        public override async Task RedoAsync() {
            foreach (HistoryAction action in this.Actions) {
                await action.RedoAsync();
            }
        }

        public override void OnRemoved() {
            foreach (HistoryAction action in this.Actions) {
                action.OnRemoved();
            }
        }
    }
}