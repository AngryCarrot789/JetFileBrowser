using System.Threading.Tasks;

namespace JetFileBrowser.Drop {
    /// <summary>
    /// An interface for an object that can handle one or more files being dropped
    /// </summary>
    public interface IFileDropHandler {
        /// <summary>
        /// The current task. This is set after <see cref="OnFilesDropped"/> is called. This is used to check if the task is already running
        /// </summary>
        bool IsProcessingDrop { get; set; }

        /// <summary>
        /// Called when a drag drop enters the UI
        /// </summary>
        /// <param name="paths">
        /// The files that were dropped. Will not be null and will always contain
        /// at least 1 element, but does not check for duplicate paths
        /// </param>
        /// <returns>The output drop type</returns>
        DropType OnDropEnter(string[] paths);

        /// <summary>
        /// Called when the last drag drop left the UI or was cancelled
        /// </summary>
        void OnDropLeave(bool isCancelled);

        /// <summary>
        /// Called when the user releases the LMB (as in, drops the file(s))
        /// </summary>
        /// <param name="paths">
        ///     The files that were dropped. Will not be null and will always contain
        ///     at least 1 element, but does not check for duplicate paths
        /// </param>
        /// <param name="dropType"></param>
        /// <returns>A task to await. No more drops can be processed until this is completed</returns>
        Task OnFilesDropped(string[] paths, DropType dropType);
    }
}