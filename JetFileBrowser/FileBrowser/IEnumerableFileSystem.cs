using System.Collections.Generic;
using System.Threading.Tasks;
using JetFileBrowser.FileBrowser.FileTree;

namespace JetFileBrowser.FileBrowser {
    /// <summary>
    /// An interface that supports directly enumerating an entry's content without actually modifying the entry
    /// </summary>
    public interface IEnumerableFileSystem {
        Task<IAsyncEntryEnumerator> EnumerateContent(TreeEntry entry);
    }
}