using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

using Kosinka.Attributes;
using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;

using Microsoft.Extensions.DependencyInjection;
using Avalonia.Media;

namespace Kosinka.Converters
{
    internal class CollectionToImageConverter : IValueConverter
    {
        private static IGetImageOfCard GetImageOfcard = ServiceHelper.ServiceProvider.GetRequiredService<IGetImageOfCard>();
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {

            if(value is IEnumerable<Card> cards)
            {
                if (cards.Any())
                    return GetImageOfcard.GetImage(cards.Last());
                else
                    return Colors.Transparent;
            }
            else
            {
                throw new ArgumentException("Ожидается IEnumerable<Card>");
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
