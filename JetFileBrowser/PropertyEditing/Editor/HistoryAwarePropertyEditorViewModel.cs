using System;
using JetFileBrowser.History;
using JetFileBrowser.History.ViewModels;

namespace JetFileBrowser.PropertyEditing.Editor {
    public abstract class HistoryAwarePropertyEditorViewModel : BasePropertyEditorViewModel {
        protected HistoryManagerViewModel HistoryManager;

        protected HistoryAwarePropertyEditorViewModel(Type applicableType) : base(applicableType) {
        }

        protected override void OnHandlersLoaded() {
            this.HistoryManager = HistoryManagerViewModel.Instance;
            base.OnHandlersLoaded();
        }

        protected override void OnClearHandlers() {
            this.HistoryManager = null;
            base.OnClearHandlers();
        }

        public bool IsChangingAny() {
            foreach (object handler in this.Handlers) {
                if (handler is IHistoryHolder holder && holder.IsHistoryChanging) {
                    return true;
                }
            }

            return false;
        }
    }
}