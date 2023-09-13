using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using JetFileBrowser.Drop;
using JetFileBrowser.FileBrowser.FileTree.Events;
using JetFileBrowser.FileBrowser.FileTree.Physical;
using JetFileBrowser.Utils;

namespace JetFileBrowser.FileBrowser.FileTree {
    /// <summary>
    /// A class that stores a tree of virtual files, while supporting things like drag drop, selection, etc.
    /// </summary>
    public class FileTreeViewModel : BaseViewModel, IFileDropHandler {
        private List<OpenFileEventHandler> openFileHandlers;
        private List<NavigateToFileEventHandler> navigateToFileHandlers;

        bool IFileDropHandler.IsProcessingDrop { get; set; }

        public AsyncRelayCommand<TreeEntry> OpenItemCommand { get; }

        public TreeEntry Root { get; }

        public ObservableCollection<TreeEntry> SelectedItems { get; }

        public event NavigateToFileEventHandler NavigateToItem {
            add => HandlerList.AddHandler(ref this.navigateToFileHandlers, value);
            remove => HandlerList.RemoveHandler(ref this.navigateToFileHandlers, value);
        }

        public event OpenFileEventHandler OpenFile {
            add => HandlerList.AddHandler(ref this.openFileHandlers, value);
            remove => HandlerList.RemoveHandler(ref this.openFileHandlers, value);
        }

        public FileTreeViewModel() {
            this.Root = new RootTreeEntry();
            this.Root.SetFileTree(this);
            this.SelectedItems = new ObservableCollection<TreeEntry>();
            this.OpenItemCommand = new AsyncRelayCommand<TreeEntry>(this.OpenFileAction);
        }

        private class RootTreeEntry : TreeEntry {
            public RootTreeEntry() : base(true) {
            }
        }

        private async Task OpenFileAction(TreeEntry item) {
            if (item == this.Root || this.openFileHandlers == null) {
                return;
            }

            // await HandlerList.HandleAsync(this.openFileHandlers, item, (x, y) => x(y));
            foreach (OpenFileEventHandler handler in this.openFileHandlers) {
                await handler(item);
            }
        }

        public DropType OnDropEnter(string[] paths) {
            Debug.WriteLine("Drop Enter! " + string.Join(", ", paths));
            return DropType.All;
        }

        public void OnDropLeave(bool isCancelled) {
            Debug.WriteLine("Drop Leave! " + (isCancelled ? "Cancelled" : "Not cancelled"));
        }

        public Task OnFilesDropped(string[] paths, DropType dropType) {
            foreach (string path in paths) {
                if (Directory.Exists(path)) {
                    this.Root.AddItemCore(Win32FileSystem.Instance.ForDirectory(path));
                }
                else if (File.Exists(path)) {
                    this.Root.AddItemCore(Win32FileSystem.Instance.ForFile(path));
                }
            }

            Debug.WriteLine("Dropped! " + string.Join(", ", paths));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the user tries to navigate to the given file. May be a file or folder
        /// </summary>
        public async Task OnNavigate(TreeEntry file) {
            await HandlerList.HandleAsync(this.navigateToFileHandlers, file, (x, y) => x(y));
        }
    }
}