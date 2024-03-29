using System.Collections.Generic;
using System.Windows;
using JetFileBrowser.WPF.Utils;

namespace JetFileBrowser.WPF.Shortcuts.Bindings {
    public class InputStateCollection : FreezableCollection<InputStateBinding> {
        public static readonly DependencyProperty CollectionProperty =
            DependencyProperty.RegisterAttached(
                "Collection",
                typeof(InputStateCollection),
                typeof(InputStateCollection),
                new FrameworkPropertyMetadata(null));

        public InputStateCollection() {
            // ((INotifyCollectionChanged) this).CollectionChanged += this.OnCollectionChanged;
        }

        // private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
        //
        // }

        /// <summary>
        /// Sets the attached <see cref="InputStateCollection"/> for an element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCollection(DependencyObject element, InputStateCollection value) => element.SetValue(CollectionProperty, value);

        /// <summary>
        /// Gets the attached <see cref="InputStateCollection"/> for an element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static InputStateCollection GetCollection(DependencyObject element) => (InputStateCollection) element.GetValue(CollectionProperty);

        public static List<InputStateBinding> GetInputStateBindingHierarchy(DependencyObject source) {
            List<InputStateBinding> list = new List<InputStateBinding>();
            do {
                object localValue = source.ReadLocalValue(CollectionProperty);
                if (localValue is InputStateCollection collection && collection.Count > 0) {
                    list.AddRange(collection);
                }
            } while ((source = VisualTreeUtils.GetParent(source)) != null);
            return list;
        }

        public static void GetInputStateBindingHierarchy(DependencyObject source, Dictionary<string, List<InputStateBinding>> map) {
            do {
                object localValue = source.ReadLocalValue(CollectionProperty);
                if (localValue is InputStateCollection collection && collection.Count > 0) {
                    foreach (InputStateBinding binding in collection) {
                        if (!string.IsNullOrWhiteSpace(binding.InputStatePath)) {
                            if (!map.TryGetValue(binding.InputStatePath, out var list))
                                map[binding.InputStatePath] = list = new List<InputStateBinding>();
                            list.Add(binding);
                        }
                    }
                }
            } while ((source = VisualTreeUtils.GetParent(source)) != null);
        }
    }
}