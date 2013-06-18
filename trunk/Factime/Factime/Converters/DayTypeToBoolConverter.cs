using System;
using System.Globalization;
using System.Windows;
using Factime.Models;
using UseAbilities.WPF.Converters.Base;

namespace Factime.Converters
{
    public class DayTypeToBoolConverter : ConverterBase<DayTypeToBoolConverter>
    {
        public DayTypeToBoolConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DayType) && !(parameter is DayType)) throw new Exception(" Value or parametr is not DayType!");

            var displayType = (DayType)value;
            var parametrValue = (DayType)parameter;

            return displayType == parametrValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool) && !(parameter is DayType)) throw new Exception(" Value is not bool or parametr is not DayType!");

            if((bool)value) return (DayType)parameter;
            return DependencyProperty.UnsetValue;
        }
    }
}
