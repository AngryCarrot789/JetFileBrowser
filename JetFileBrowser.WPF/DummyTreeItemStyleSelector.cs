using System.Windows;
using System.Windows.Controls;
using JetFileBrowser.FileBrowser.FileTree;

namespace JetFileBrowser.WPF {
    public class DummyTreeItemStyleSelector : StyleSelector {
        public Style WithDummyStyle { get; set; }

        public Style DefaultStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container) {
            if (item is TreeEntry entry && entry.CanHoldItems)
                return this.WithDummyStyle;
            return this.DefaultStyle;
        }
    }
}