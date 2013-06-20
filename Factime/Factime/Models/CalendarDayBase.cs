using System;

namespace Factime.Models
{
    public abstract class CalendarDayBase
    {
        private DateTime _date;
        public virtual DateTime Date
        {
            get
            {
                return _date.Date;
            }
            set
            {
                _date = value;
            }
        }

        public virtual DayType Type
        {
            get; 
            set;
        }

        private bool _isStateHoliday;
        public virtual bool IsStateHoliday
        {
            get
            {
                if (_isStateHoliday) Console.WriteLine(@"get IsStateHoliday = true for: {0}", Date); 
                return _isStateHoliday;
            }
            set
            {
                if (value) Console.WriteLine(@"set IsStateHoliday = true for: {0}", Date); 
                _isStateHoliday = value;
                Type = value ? DayType.Holiday : DayType.Workday;
            }
        }
    }
}
