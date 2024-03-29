﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JetFileBrowser.PropertyEditing;

namespace JetFileBrowser.WPF.PropertyEditing.Converters {
    public class PropertyItemToolTipConverter : IMultiValueConverter {
        public static PropertyItemToolTipConverter Instance { get; } = new PropertyItemToolTipConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }

            string description = (string) values[0];
            if (string.IsNullOrEmpty(description)) {
                description = "No description available";
            }

            PropertyGroupViewModel parent = (PropertyGroupViewModel) values[1];
            if (parent == null || parent.IsRoot) {
                return description;
            }
            else {
                return $"{description}. This is parented to '{parent.Id}'";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
