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
                return _isStateHoliday;
            }
            set
            {
                _isStateHoliday = value;
                Type = value ? DayType.Holiday : DayType.Workday;
            }
        }
    }
}
