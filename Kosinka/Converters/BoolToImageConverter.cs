using Avalonia.Data;
using Avalonia.Data.Converters;

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
    internal class BoolToImageConverter : IValueConverter
    {
        public static readonly IGetImageOfCard getImageOfCard;
        static BoolToImageConverter()
        {
            getImageOfCard = UseInAppAttribute.ServiceProvider.GetRequiredService<IGetImageOfCard>();
        }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is bool b && parameter is Card card)
            {
                if(b)
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
