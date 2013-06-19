using System;
using System.Globalization;
using UseAbilities.WPF.Converters.Base;

namespace Factime.Converters
{
    public class StringToTimeSpanConverter : ConverterBase<StringToTimeSpanConverter>
    {
        public StringToTimeSpanConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString();
            if (stringValue.Length > 5) stringValue = stringValue.Remove(5);

            return stringValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            TimeSpan result;
            if (string.IsNullOrEmpty(stringValue)) throw new ArgumentException("Value is not string or empty");

            if (stringValue.Length > 5) stringValue = stringValue.Remove(5);
            if (stringValue.Length < 5) stringValue = stringValue.Insert(1, ":");

            if (!TimeSpan.TryParse(stringValue, out result)) throw new Exception("Can't parse TimeSpan from string");

            return result;
        }
    }
}
