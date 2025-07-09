using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

using System;
using System.Globalization;

namespace Kosinka.Converters
{
    internal class AvaloniaObjectToCanvasLeftConverter : IValueConverter
    {
        protected int counter = 1;
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is AvaloniaObject obj)
            {
                ++counter;
                return Canvas.GetLeft(obj)+160*(counter/2);
                
            }
            else
            {
                throw new ArgumentException("Ожидался AvaloniaObject для Canvas.Left");
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
