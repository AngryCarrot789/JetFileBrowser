namespace JetFileBrowser.History {
    public interface IHistoryManager {
        void AddAction(HistoryAction action, string information = null);
    }
}