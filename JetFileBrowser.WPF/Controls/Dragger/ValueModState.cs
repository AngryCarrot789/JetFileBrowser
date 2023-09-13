using System;

namespace JetFileBrowser.WPF.Controls.Dragger {
    public enum ValueModState {
        /// <summary>
        /// Nothing is happening
        /// </summary>
        None,

        /// <summary>
        /// Value is now being modified
        /// </summary>
        Begin,

        /// <summary>
        /// Value has finished being modified
        /// </summary>
        Finish,

        /// <summary>
        /// Value modification was cancelled and should be reverted back to value before <see cref="Begin"/>
        /// </summary>
        Cancelled
    }

    public static class ValueModStateExtensions {
        public static bool IsBegin(this ValueModState state) {
            return state == ValueModState.Begin;
        }

        public static bool IsFinish(this ValueModState state) {
            return state != ValueModState.Begin && state != ValueModState.None;
        }

        public static void Apply(this ValueModState state, ref bool isBeginState, Action isBegin, Action isFinish, Action isCancelled = null) {
            switch (state) {
                case ValueModState.None: return;
                case ValueModState.Begin: {
                    if (!isBeginState) {
                        isBeginState = true;
                        isBegin?.Invoke();
                    }

                    break;
                }
                default: {
                    if (isBeginState) {
                        isBeginState = false;
                        if (state == ValueModState.Cancelled) {
                            (isCancelled ?? isFinish)?.Invoke();
                        }
                        else {
                            isFinish?.Invoke();
                        }
                    }

                    break;
                }
            }
        }
    }
}