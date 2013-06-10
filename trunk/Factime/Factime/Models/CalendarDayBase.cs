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
    }
}
