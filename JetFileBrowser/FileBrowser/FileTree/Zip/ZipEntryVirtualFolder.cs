namespace JetFileBrowser.FileBrowser.FileTree.Zip {
    public class ZipEntryVirtualFolder : BaseZipVirtualFile {
        public override bool CanHoldItems => true;

        public ZipEntryVirtualFolder(string fullZipPath) : base(fullZipPath) {
        }
    }
}