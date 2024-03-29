using System;
using JetFileBrowser.FileBrowser.FileTree.Interfaces;
using JetFileBrowser.FileBrowser.FileTree.Physical;

namespace JetFileBrowser.FileBrowser.FileTree {
    public static class EntrySorters {
        public static readonly Comparison<TreeEntry> CompareFileName = (a, b) => {
            if (a is IFileName nA) {
                if (b is IFileName nB) {
                    return string.Compare(nA.FileName, nB.FileName);
                }
                else {
                    return -1;
                }
            }
            else if (b is IFileName) {
                return 1;
            }
            else {
                return 0;
            }
        };

        public static readonly Comparison<TreeEntry> CompareDirectoryAndFileName = (a, b) => {
            if (a.IsDirectory) {
                if (b.IsDirectory) {
                    return CompareFileName(a, b);
                }
                else {
                    return -1; // A comes before B
                }
            }
            else if (b is PhysicalVirtualFolder) {
                return 1; // A comes after B
            }
            else {
                return CompareFileName(a, b);
            }
        };
    }
}