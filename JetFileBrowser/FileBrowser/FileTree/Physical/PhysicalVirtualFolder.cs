using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using JetFileBrowser.Drop;
using JetFileBrowser.Utils;

namespace JetFileBrowser.FileBrowser.FileTree.Physical {
    /// <summary>
    /// A class for a physical virtual folder
    /// </summary>
    public class PhysicalVirtualFolder : BasePhysicalVirtualFile, IFileDropHandler {
        private readonly Dictionary<string, TreeEntry> nameToEntry;

        public bool IsProcessingDrop { get; set; }

        public PhysicalVirtualFolder() : base(true) {
            this.nameToEntry = new Dictionary<string, TreeEntry>();
        }

        protected override void OnItemAdded(int index, TreeEntry entry) {
            base.OnItemAdded(index, entry);
            if (!entry.TryGetDataValue(Win32FileSystem.FilePathKey, out string path)) {
                throw new Exception("Cannot add a non-physical file (or a file without a path) to a physical folder");
            }

            string name = Path.GetFileName(path);
            if (this.nameToEntry.ContainsKey(name)) {
                throw new Exception("Cannot add the same file twice at: " + path);
            }

            this.nameToEntry[name] = entry;
        }

        protected override void OnItemRemoved(int index, TreeEntry entry) {
            base.OnItemRemoved(index, entry);
            if (!entry.TryGetDataValue(Win32FileSystem.FilePathKey, out string path)) {
                return; // ...
            }

            string name = Path.GetFileName(path);
            this.nameToEntry.Remove(name);
        }

        public TreeEntry GetEntryByName(string fileName) {
            foreach (TreeEntry entry in this.Items) {
                if (!entry.TryGetDataValue(Win32FileSystem.FilePathKey, out string path)) {
                    Debug.WriteLine("[WARNING] A child in a physical virtual folder had no path associated: " + entry.GetType());
                    continue;
                }

                string name = Path.GetFileName(path);
                if (name == null && fileName == null || name.EqualsIgnoreCase(fileName)) {
                    return entry;
                }
            }

            return null;
        }

        public DropType OnDropEnter(string[] paths) {
            return DropType.Link;
        }

        public void OnDropLeave(bool isCancelled) {

        }

        public async Task OnFilesDropped(string[] paths, DropType dropType) {
            if (this.FileSystem != Win32FileSystem.Instance) {
                return;
            }

            foreach (string path in paths) {
                string name = Path.GetFileName(path);
                if (this.GetEntryByName(name) == null) {
                    this.AddItemCore(Win32FileSystem.Instance.ForFilePath(path));
                }
            }
        }
    }
}