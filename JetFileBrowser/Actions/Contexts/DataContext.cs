using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JetFileBrowser.Actions.Contexts {
    /// <summary>
    /// An implementation of <see cref="IDataContext"/>. This class is mutable even though the interface is not meant to be
    /// </summary>
    public class DataContext : IDataContext {
        private static readonly Dictionary<string, object> EmptyDictionary = new Dictionary<string, object>();

        public List<object> ContextList { get; }

        public Dictionary<string, object> EntryMap { get; set; }

        IReadOnlyList<object> IDataContext.Context => this.ContextList;

        IReadOnlyDictionary<string, object> IDataContext.Entries => this.EntryMap ?? EmptyDictionary;

        public DataContext() {
            this.ContextList = new List<object>();
        }

        public DataContext(object primaryContext) : this() {
            this.AddContext(primaryContext);
        }

        public T GetContext<T>() {
            this.TryGetContext(out T value); // value will be default or null
            return value;
        }

        public bool TryGetContext<T>(out T value) {
            foreach (object obj in this.ContextList) {
                if (obj is T t) {
                    value = t;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public bool TryGetContext(Type type, out object value) {
            return (value = this.ContextList.First(type.IsInstanceOfType)) != null;
        }

        public bool HasContext<T>() {
            return this.ContextList.Any(x => x is T);
        }

        public bool TryGet<T>(string key, out T value) {
            if (key == null) {
                throw new ArgumentNullException(nameof(key), "Key cannot be null");
            }

            if (this.EntryMap != null && this.EntryMap.TryGetValue(key, out object data) && data is T t) {
                value = t;
                return true;
            }

            value = default;
            return false;
        }

        public bool ContainsKey(string key) {
            return this.TryGet<object>(key, out _);
        }

        public bool HasFlag(string key) {
            return this.TryGet(key, out bool value) && value;
        }

        public T Get<T>(string key) {
            this.TryGet(key, out T value);
            return value; // ValueType will be default, object will be null
        }

        public void AddContext(object context) {
            this.ContextList.Add(context);
        }

        public void Set(string key, object value) {
            if (key == null) {
                throw new ArgumentNullException(nameof(key), "Key cannot be null");
            }

            if (value == null) {
                this.EntryMap?.Remove(key);
            }
            else {
                if (this.EntryMap == null) {
                    this.EntryMap = new Dictionary<string, object>();
                }

                this.EntryMap[key] = value;
            }
        }

        public void Merge(IDataContext ctx) {
            foreach (object value in ctx.Context) {
                this.ContextList.Add(value);
            }

            IReadOnlyDictionary<string, object> entries;
            if (ctx is DataContext ctxImpl) {
                // slight optimisation; no need to deconstruct KeyValuePairs into tuples
                if (ctxImpl.EntryMap != null && ctxImpl.EntryMap.Count > 0) {
                    if (this.EntryMap == null) {
                        this.EntryMap = new Dictionary<string, object>(ctxImpl.EntryMap);
                    }
                    else {
                        foreach (KeyValuePair<string, object> entry in ctxImpl.EntryMap) {
                            this.EntryMap[entry.Key] = entry.Value;
                        }
                    }
                }
            }
            else if ((entries = ctx.Entries).Count > 0) {
                // IReadOnlyDictionary was added after Dictionary so the ctor won't accept the read only version :'(
                Dictionary<string, object> map = this.EntryMap ?? (this.EntryMap = new Dictionary<string, object>());
                foreach (KeyValuePair<string, object> entry in entries) {
                    map[entry.Key] = entry.Value;
                }
            }
        }
    }
}