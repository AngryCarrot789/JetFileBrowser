using System.Windows;
using System.Windows.Controls;

namespace JetFileBrowser.WPF.Explorer {
    internal class FileTreeControl : TreeView {
        public FileTreeControl() {

        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is FileTreeItem;

        protected override DependencyObject GetContainerForItemOverride() => new FileTreeItem();
    }
}