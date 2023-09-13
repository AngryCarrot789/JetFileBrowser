using System;

namespace JetFileBrowser.History.Exceptions {
    /// <summary>
    /// An exception that is thrown when an attempt to call <see cref="HistoryAction.UndoAsync"/> a second time before redoing.
    /// Undo and redo must occur subsequently. This exception would only be thrown if there's a bug in the history management system
    /// </summary>
    public class NotRedoneException : InvalidOperationException {
        private static readonly string FunctionName = $"{nameof(HistoryAction)}.{nameof(HistoryAction.UndoAsync)}";

        public NotRedoneException() : base("Excessive calls to " + FunctionName + ". A redo needs to be called before undo") {
        }
    }
}