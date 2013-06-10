using System;

namespace Factime.Models
{
    public class CalendarDay : CalendarDayBase
    {
        public TimeSpan Start
        {
            get; 
            set;
        }

        public TimeSpan End
        {
            get;
            set;
        }
    }
}
