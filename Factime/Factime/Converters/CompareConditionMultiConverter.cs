using System;
using System.Globalization;
using UseAbilities.WPF.Converters.Base;

namespace Factime.Converters
{
    public class CompareConditionMultiConverter : MultiConverterBase<CompareConditionMultiConverter>
    {
        public CompareConditionMultiConverter()
        {
            //
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) return true;
            return (int)values[0] == (int)values[1];
        }
    }
}
