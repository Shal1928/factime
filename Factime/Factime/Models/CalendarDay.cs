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

        public void Fill(CalendarDay calendarDay)
        {
            Start = calendarDay.Start;
            End = calendarDay.End;
            Type = calendarDay.Type;
            IsStateHoliday = calendarDay.IsStateHoliday;
        }
    }
}
