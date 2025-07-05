using System;
using System.Globalization;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Kosinka.Converters
{
    internal class AvaloniaObjectToCanvasTopConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is AvaloniaObject obj)
            {
                return Canvas.GetTop(obj);
            }
            else
            {
                throw new ArgumentException("Ожидался AvaloniaObject для Canvas.Top");
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
