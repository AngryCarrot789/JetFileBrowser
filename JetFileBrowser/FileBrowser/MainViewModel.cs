using System;
using System.IO;
using System.Threading.Tasks;
using JetFileBrowser.FileBrowser.FileTree;
using JetFileBrowser.FileBrowser.FileTree.Physical;

namespace JetFileBrowser.FileBrowser {
    public class MainViewModel : BaseViewModel {
        public FileTreeViewModel FileTree { get; }

        public TreeEntry CurrentFolder { get; private set; }

        public AsyncRelayCommand OpenFolderCommand { get; }

        public MainViewModel() {
            this.OpenFolderCommand = new AsyncRelayCommand(this.OpenFolderAction);
            this.FileTree = new FileTreeViewModel();
            this.FileTree.OpenFile += this.ExplorerOnOpenFile;
            this.FileTree.NavigateToItem += this.FileTreeOnNavigateToItem;
            this.CurrentFolder = this.FileTree.Root;
        }

        private Task FileTreeOnNavigateToItem(TreeEntry file) {
            if (file.IsDirectory) {
                this.CurrentFolder = file;
            }
            else if (file.Parent != null) {
                this.CurrentFolder = file.Parent;
            }
            else {
                this.CurrentFolder = this.FileTree.Root;
            }

            this.RaisePropertyChanged(nameof(this.CurrentFolder));

            return Task.CompletedTask;
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