using System;
using System.Globalization;
using UseAbilities.WPF.Converters.Base;

namespace Factime.Converters
{
    public class StateHolidayMultiConverter : MultiConverterBase<StateHolidayMultiConverter>
    {
        public StateHolidayMultiConverter()
        {
            //
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(values[0] is bool) && !(values[1] is bool)) throw new Exception("Values is not bool!");
            if((bool) values[0]) return true;

            return (bool) values[1];
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { false, value };
        }
    }
}
