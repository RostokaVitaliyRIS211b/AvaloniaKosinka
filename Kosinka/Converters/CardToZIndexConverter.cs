using System;
using System.Globalization;
using Avalonia.Data.Converters;

using Kosinka.Core.Model;

namespace Kosinka.Converters
{
    internal class CardToZIndexConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return 0;

            if(value is Card card)
            {
                return card.ZIndex;
            }
            else
            {
                throw new ArgumentException("ќжидалс€ Card в качетве value");
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
