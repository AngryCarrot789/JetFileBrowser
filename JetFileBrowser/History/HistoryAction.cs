using System;
using System.Threading.Tasks;

namespace JetFileBrowser.History {
    /// <summary>
    /// An action that can be undone and then also redone (but only after being undone)
    /// </summary>
    public abstract class HistoryAction {
        /// <summary>
        /// Undoes the action. E.g. you created a file. Calling this will then delete that file
        /// </summary>
        /// <returns></returns>
        public abstract Task UndoAsync();

        /// <summary>
        /// Redoes an action that was undone. E.g. you created a file and then deleted
        /// it, redoing will then re-create that file as if you never undid anything
        /// </summary>
        /// <returns></returns>
        public abstract Task RedoAsync();

        /// <summary>
        /// Called once this history action is no longer reachable, e.g. it is removed from the history queue because there were too many actions to undo/redo
        /// <para>
        /// This should clean up any resources that, for example, implement <see cref="IDisposable"/>. This method should also not throw an exception
        /// </para>
        /// </summary>
        /// <returns></returns>
        public virtual void OnRemoved() {

        }
    }
}