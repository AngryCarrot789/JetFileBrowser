using System;

namespace JetFileBrowser.ThreadSafety {
    public struct Locker : IDisposable {
        private readonly MutexArray array;

        public Locker(MutexArray array, LockType type) {
            this.array = array;
        }

        public void Dispose() {
        }
    }
}