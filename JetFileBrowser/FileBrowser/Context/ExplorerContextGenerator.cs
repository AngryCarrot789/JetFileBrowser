using System.Collections.Generic;
using JetFileBrowser.Actions.Contexts;
using JetFileBrowser.AdvancedContextService;
using JetFileBrowser.FileBrowser.FileTree;
using JetFileBrowser.FileBrowser.FileTree.Physical;

namespace JetFileBrowser.FileBrowser.Context {
    public class ExplorerContextGenerator : IContextGenerator {
        public static ExplorerContextGenerator Instance { get; } = new ExplorerContextGenerator();

        public RelayCommand<string> OpenInExplorerCommand { get; }

        public RelayCommand<string> CopyStringCommand { get; }

        public ExplorerContextGenerator() {
            this.OpenInExplorerCommand = new RelayCommand<string>((x) => {
                if (!string.IsNullOrEmpty(x))
                    IoC.ExplorerService.OpenFileInExplorer(x);
            });

            this.CopyStringCommand = new RelayCommand<string>((x) => {
                if (!string.IsNullOrEmpty(x))
                    IoC.Clipboard.SetText(x);
            });
        }

        public void Generate(List<IContextEntry> list, IDataContext context) {
            if (!context.TryGetContext(out TreeEntry item)) {
                return;
            }

            FileTreeViewModel explorer = item.FileTree;
            if (explorer != null || context.TryGetContext(out explorer)) {
                if (item.ContainsKey(Win32FileSystem.FilePathKey)) {
                    list.Add(new CommandContextEntry("Open", explorer.OpenItemCommand, item));
                }
            }

            if (item.TryGetDataValue(Win32FileSystem.FilePathKey, out string path)) {
                list.Add(new CommandContextEntry("Open in Explorer", this.OpenInExplorerCommand, path));
                list.Add(new CommandContextEntry("Copy Path", this.CopyStringCommand, path));
            }
        }
    }
}