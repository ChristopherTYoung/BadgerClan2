using System;
using System.Collections.Generic;
using BadgerClan.Shared;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.Converters
{
    internal class StrategyToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (StrategyType)value! switch
            {
                StrategyType.MyStrategy => "My Strategy",
                StrategyType.OtherStrategy => "Other Strategy",
                StrategyType.DoNothing => "Do Nothing",
                StrategyType.CircleStrategy => "Circle Strategy",
                _ => ""
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
