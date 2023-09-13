using System.Threading.Tasks;
using JetFileBrowser.Views.Windows;

namespace JetFileBrowser.WPF.Views {
    public class BaseWindow : WindowViewBase, IWindow {
        public bool IsOpen => base.IsLoaded;

        public BaseWindow() {
            this.SetToCenterOfScreen();
        }

        public void CloseWindow() {
            this.Close();
        }

        public Task CloseWindowAsync() {
            return base.CloseAsync();
        }
    }
}