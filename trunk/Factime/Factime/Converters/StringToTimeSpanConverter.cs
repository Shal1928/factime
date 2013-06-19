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
            if (stringValue.Length < 5)
            {
                if (stringValue.Contains(":"))
                {
                    const char splitChar = ':';
                    var stringArray = stringValue.Split(splitChar);
                    if (stringArray[0].Length < 2) stringArray[0] = "0" + stringArray[0];
                    if (stringArray[1].Length < 2) stringArray[1] = "0" + stringArray[1];

                    stringValue = stringArray[0] + splitChar + stringArray[1];
                }
                else
                {
                    var i = 2;
                    if (stringValue.Length == 1) stringValue = "0" + stringValue + "00";
                    if (1 < stringValue.Length && stringValue.Length <= 3) i = 1;
                    
                    stringValue = stringValue.Insert(i, ":");
                }
            }

            if (!TimeSpan.TryParse(stringValue, out result)) return "00:00"; //throw new Exception("Can't parse TimeSpan from string");

            return result;
        }
    }
}
