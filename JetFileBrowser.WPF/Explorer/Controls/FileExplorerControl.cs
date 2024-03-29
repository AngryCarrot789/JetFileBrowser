﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using JetFileBrowser.Actions.Contexts;
using JetFileBrowser.FileBrowser.Explorer;
using JetFileBrowser.FileBrowser.Explorer.ViewModes;
using JetFileBrowser.FileBrowser.FileTree;

namespace JetFileBrowser.WPF.Explorer.Controls {
    public class FileExplorerControl : Control {
        public static readonly DependencyProperty ViewModeProperty =
            DependencyProperty.Register(
                "ViewMode",
                typeof(IExplorerViewMode),
                typeof(FileExplorerControl),
                new FrameworkPropertyMetadata(ListBasedViewMode.Instance, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty CurrentFolderProperty =
            DependencyProperty.Register(
                "CurrentFolder",
                typeof(TreeEntry),
                typeof(FileExplorerControl),
                new PropertyMetadata(null, (d, e) => ((FileExplorerControl) d).OnCurrentFolderPropertyChanged((TreeEntry) e.OldValue, (TreeEntry) e.NewValue)));

        public IExplorerViewMode ViewMode {
            get => (IExplorerViewMode)this.GetValue(ViewModeProperty);
            set => this.SetValue(ViewModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the folder that this explorer is currently located in.
        /// <see cref="TreeEntry.IsDirectory"/> must be true, otherwise an exception is thrown
        /// </summary>
        public TreeEntry CurrentFolder {
            get => (TreeEntry) this.GetValue(CurrentFolderProperty);
            set => this.SetValue(CurrentFolderProperty, value);
        }

        public FileExplorerControl() {
        }

        private async void OnCurrentFolderPropertyChanged(TreeEntry oldEntry, TreeEntry newEntry) {
            if (newEntry == null) {
                return;
            }

            if (!newEntry.IsDirectory) {
                throw new Exception(nameof(this.CurrentFolder) + " must be able to hold items");
            }

            if (!newEntry.IsContentLoaded && newEntry.FileSystem != null) {
                try {
                    await newEntry.FileSystem.RefreshContent(newEntry);
                }
                finally {
                    newEntry.IsContentLoaded = true;
                }
            }
        }
    }
}
