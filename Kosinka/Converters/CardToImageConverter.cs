using Avalonia.Data;
using Avalonia.Data.Converters;

using Cosinka.Viewmodel.Realizations;

using Kosinka.Attributes;
using Kosinka.Core.Interfaces;
using Kosinka.Core.Model;
using Kosinka.Helpers;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Converters
{
    internal class CardToImageConverter : IValueConverter
    {
        private static IGetImageOfCard getImageOfCard = ServiceHelper.ServiceProvider.GetRequiredService<IGetImageOfCard>();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            
            if (value is Card card)
            {
                if(card.IsOpen)
                {
                    return getImageOfCard.GetImage(card);
                }
                else
                {
                    return ImageHelper.BackOfCard; 
                }
            }
            return new BindingNotification(new ArgumentException("Expected a bool value"),BindingErrorType.DataValidationError);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
