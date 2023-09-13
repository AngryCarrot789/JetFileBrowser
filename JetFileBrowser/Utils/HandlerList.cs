using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JetFileBrowser.Utils {
    // public class TaskHandlerList : IReadOnlyList<Task> {
    //     private readonly List<Task> list;
    //
    //     public int Count => this.list.Count;
    //
    //     public Task this[int index] => this.list[index];
    //
    //     public TaskHandlerList() {
    //         this.list = new List<Task>();
    //     }
    //
    //     public void Add(Task task) {
    //         if (task == null)
    //             throw new ArgumentNullException(nameof(task));
    //         if (this.list.Contains(task))
    //             return;
    //         this.list.Add(task);
    //     }
    //
    //     public bool Remove(Task task) {
    //         if (task == null)
    //             throw new ArgumentNullException(nameof(task));
    //         return this.list.Remove(task);
    //     }
    //
    //     public List<Task>.Enumerator GetEnumerator() => this.list.GetEnumerator();
    //
    //     IEnumerator<Task> IEnumerable<Task>.GetEnumerator() => this.list.GetEnumerator();
    //
    //     IEnumerator IEnumerable.GetEnumerator() => this.list.GetEnumerator();
    // }

    public static class HandlerList {
        public static void AddHandler<T>(ref List<T> list, T value) where T : Delegate {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (list == null) {
                list = new List<T>();
            }
            else if (list.Contains(value)) {
                return;
            }

            list.Add(value);
        }

        public static void RemoveHandler<T>(ref List<T> list, T value) where T : Delegate {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            list?.Remove(value);
        }

        public static async Task HandleAsync<T>(List<T> list, Func<T, Task> executor) where T : Delegate {
            if (list == null || list.Count < 1)
                return;
            foreach (T handler in list)
                await executor(handler);
        }

        public static async Task HandleAsync<T, P0>(List<T> list, P0 p0, Func<T, P0, Task> executor) where T : Delegate {
            if (list == null || list.Count < 1)
                return;
            foreach (T handler in list)
                await executor(handler, p0);
        }

        public static async Task HandleAsync<T, P0, P1>(List<T> list, P0 p0, P1 p1, Func<T, P0, P1, Task> executor) where T : Delegate {
            if (list == null || list.Count < 1)
                return;
            foreach (T handler in list)
                await executor(handler, p0, p1);
        }
    }
}