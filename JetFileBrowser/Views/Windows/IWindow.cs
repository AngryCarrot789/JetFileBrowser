using System.Threading.Tasks;

namespace JetFileBrowser.Views.Windows {
    public interface IWindow : IViewBase {
        void CloseWindow();

        Task CloseWindowAsync();

        bool IsOpen { get; }
    }
}