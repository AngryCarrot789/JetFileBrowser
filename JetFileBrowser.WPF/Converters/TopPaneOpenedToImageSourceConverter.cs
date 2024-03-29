﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace JetFileBrowser.WPF.Converters {
    public class TopPaneOpenedToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool paneOpened) {
                if ((bool) value)
                    return new Uri(@"/MCNBTEditor;component/Resources/topTabsClose.png", UriKind.Relative);
                else
                    return new Uri(@"/MCNBTEditor;component/Resources/topTabsOpen.png", UriKind.Relative);
            }
            else
                return new Uri(@"/MCNBTEditor;component/Resources/topTabsOpen.png", UriKind.Relative);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return false;
        }
    }
}