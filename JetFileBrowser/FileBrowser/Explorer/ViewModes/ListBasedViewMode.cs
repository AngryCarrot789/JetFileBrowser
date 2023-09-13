namespace JetFileBrowser.FileBrowser.Explorer.ViewModes {
    public class ListBasedViewMode : BaseViewModel, IExplorerViewMode {
        public static ListBasedViewMode Instance { get; } = new ListBasedViewMode();

        public string Id => "List";

        private ListBasedViewMode() {
        }
    }
}