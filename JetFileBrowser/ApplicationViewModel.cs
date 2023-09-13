using JetFileBrowser.Settings.ViewModels;

namespace JetFileBrowser {
    public class ApplicationViewModel : BaseViewModel {
        public ApplicationSettings Settings { get; }

        public ApplicationViewModel() {
            this.Settings = new ApplicationSettings();
        }
    }
}