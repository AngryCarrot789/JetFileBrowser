using System;

namespace JetFileBrowser.Utils {
    public interface IRealDisposable : IDisposable {
        bool IsDisposed { get; }

        void Dispose(bool isDisposing);
    }
}