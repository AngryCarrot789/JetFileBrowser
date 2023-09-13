using System.Threading.Tasks;

namespace JetFileBrowser.FileBrowser.FileTree.Events {
    public delegate Task OpenFileEventHandler(TreeEntry file);
}