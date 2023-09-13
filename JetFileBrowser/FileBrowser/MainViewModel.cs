using System;
using System.IO;
using System.Threading.Tasks;
using JetFileBrowser.FileBrowser.FileTree;
using JetFileBrowser.FileBrowser.FileTree.Physical;

namespace JetFileBrowser.FileBrowser {
    public class MainViewModel : BaseViewModel {
        public FileTreeViewModel FileTree { get; }

        public AsyncRelayCommand OpenFolderCommand { get; }

        public MainViewModel() {
            this.OpenFolderCommand = new AsyncRelayCommand(this.OpenFolderAction);
            this.FileTree = new FileTreeViewModel();
            this.FileTree.OpenFile += this.ExplorerOnOpenFile;
        }

        private async Task ExplorerOnOpenFile(TreeEntry file) {
            if (file is PhysicalVirtualFile virtualFile) {
                if (!File.Exists(virtualFile.FilePath)) {
                    if (virtualFile.Parent != null) {
                        await virtualFile.Parent.RefreshAsync();
                    }
                }
                else {
                    // navigate
                }
            }
        }

        private async Task OpenFolderAction() {
            string path = await IoC.FilePicker.OpenFolder(null, "Select a folder to open");
            if (string.IsNullOrEmpty(path)) {
                return;
            }

            this.FileTree.Root.AddItemCore(Win32FileSystem.Instance.ForDirectory(path));
        }

        public async Task LoadDefaultLocation() {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (Directory.Exists(path)) {
                await Win32FileSystem.Instance.LoadContentWin32(path, this.FileTree.Root);
            }
        }
    }
}