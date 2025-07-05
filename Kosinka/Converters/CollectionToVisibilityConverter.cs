using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Kosinka.Core.Model;

namespace Kosinka.Converters
{
    internal class CollectionToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            
            if(value is IEnumerable<Card> collection)
            {
                return collection.Any();
            }
            else
            {
                throw new ArgumentException("ќжидалась коллекци€ на входе");
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new  NotImplementedException();
        }
    }
}
