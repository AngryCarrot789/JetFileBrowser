using System;
using AvalonDock.Themes;

namespace JetFileBrowser.WPF.Themes.AvalonDock {
    /// <inheritdoc/>
    public class GeneralDockTheme : Theme {
        /// <inheritdoc/>
        public override Uri GetResourceUri() {
            return new Uri("/JetFileBrowser.WPF;component/Themes/AvalonDock/GeneralDockTheme.xaml", UriKind.Relative);
        }
    }
}