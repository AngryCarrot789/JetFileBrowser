using System;

namespace JetFileBrowser.Plugins {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IgnoredPluginAttribute : Attribute {

    }
}