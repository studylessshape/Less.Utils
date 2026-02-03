using Microsoft.Xaml.Behaviors;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Less.Utils.WPF.Converters
{
    /// <summary>
    /// Get <see cref="System.Windows.DataTemplate"/> index in <see cref="ItemsControl"/> by the method <see cref="ItemContainerGenerator.IndexFromContainer(DependencyObject)"/> of <see cref="ItemsControl.ItemContainerGenerator"/>
    /// </summary>
    public class TemplateIndexInItemsControlConverter : IValueConverter
    {
        public int Append { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DependencyObject control)
            {
                var itemsControl = control.GetSelfAndAncestors().Where(p => p is ItemsControl).Select(p => (ItemsControl)p).FirstOrDefault();
                if (itemsControl == null)
                {
                    return Binding.DoNothing;
                }

                return itemsControl.ItemContainerGenerator.IndexFromContainer(control) + Append;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
