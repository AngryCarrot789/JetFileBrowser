using JetFileBrowser.Utils;

namespace JetFileBrowser.FileBrowser.FileTree.Zip {
    public class ZipEntryVirtualFolder : BaseZipVirtualFile {
        public ZipEntryVirtualFolder(string fullZipPath) : base(fullZipPath, true) {
        }

        public override void AddItemCore(TreeEntry item) {
            this.InsertItemCore(CollectionUtils.GetSortInsertionIndex(this.Items, item, EntrySorters.CompareDirectoryAndFileName), item);
        }
    }
}