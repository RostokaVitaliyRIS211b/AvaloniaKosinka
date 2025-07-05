using MsBox.Avalonia.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Helpers
{
    internal static class MessageBoxParamsHelper
    {
        public static MessageBoxStandardParams GetErrorBox(string message)
        {
            return new MessageBoxStandardParams()
            {
                WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterScreen,
                MinWidth = 300,
                ContentTitle = "Ошибка",
                Icon = MsBox.Avalonia.Enums.Icon.Error,
                ButtonDefinitions = MsBox.Avalonia.Enums.ButtonEnum.Ok,
                ContentMessage = message
            };
        }
    }
}
