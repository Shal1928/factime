using System;
using System.Globalization;
using System.Windows;
using UseAbilities.WPF.Converters.Base;

namespace Factime.Converters
{
    public class MonthToBoolConverter : ConverterBase<MonthToBoolConverter>
    {
        public MonthToBoolConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int) && !(parameter is int)) throw new Exception(" Value or parametr is not int!");

            var intValue = (int)value;
            int parametrValue;
            int.TryParse((string)parameter, out parametrValue);

            return intValue == parametrValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool) && !(parameter is int)) throw new Exception(" Value is not bool or parametr is not int!");

            int parametrValue;
            int.TryParse((string)parameter, out parametrValue);

            if ((bool)value) return parametrValue;
            return DependencyProperty.UnsetValue;
        }
    }
}
