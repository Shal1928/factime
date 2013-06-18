using System;
using System.Globalization;
using UseAbilities.WPF.Converters.Base;

namespace Factime.Converters
{
    public class ExtractMonthConverter : ConverterBase<ExtractMonthConverter>
    {
        public ExtractMonthConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime)) throw new ArgumentException("Value is not DateTime into ExtractMonthConverter");
            return ((DateTime) value).Month;
        }
    }
}
