using System;

namespace JetFileBrowser.History.Exceptions {
    /// <summary>
    /// An exception that is thrown when an attempt to call <see cref="HistoryAction.RedoAsync"/> a second time before undoing.
    /// Undo and redo must occur subsequently. This exception would only be thrown if there's a bug in the history management system
    /// </summary>
    public class NotUndoneException : InvalidOperationException {
        private static readonly string FunctionName = $"{nameof(HistoryAction)}.{nameof(HistoryAction.RedoAsync)}";

        public NotUndoneException() : base("Excessive calls to " + FunctionName + ". An undo needs to be called before redo") {
        }
    }
}