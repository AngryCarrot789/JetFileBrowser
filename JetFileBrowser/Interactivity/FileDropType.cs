using System;

namespace JetFileBrowser.Interactivity {
    [Flags]
    public enum FileDropType {
        None,
        Copy,
        Move,
        All = Copy | Move
    }
}