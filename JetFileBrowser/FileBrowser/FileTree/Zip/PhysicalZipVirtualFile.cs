using System;
using System.IO.Compression;
using JetFileBrowser.FileBrowser.FileTree.Physical;

namespace JetFileBrowser.FileBrowser.FileTree.Zip {
    /// <summary>
    /// A class for zip files (.zip, .jar, etc.)
    /// </summary>
    public class PhysicalZipVirtualFile : PhysicalVirtualFile, IZipRoot {
        public override bool CanHoldItems => true;

        public ZipArchive Archive { get; set; }

        public PhysicalZipVirtualFile(TreeFileSystem fileSystem) {
            this.FileSystem = fileSystem;
        }

        protected override void OnRemovedFromParent(TreeEntry parent) {
            base.OnRemovedFromParent(parent);
            if (this.FileSystem is IDisposable disposable) {
                disposable.Dispose();
            }
        }
    }
}