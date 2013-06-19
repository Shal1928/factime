using System;
using System.Collections.Generic;
using System.Linq;

namespace Factime.Models
{
    public class WeekWrapper : IEquatable<WeekWrapper>
    {
        public WeekWrapper(int curentMonth)
        {
            CurrentMonth = curentMonth;
        }

        public CalendarDay Monday
        {
            get; 
            set;
        }

        public CalendarDay Tuesday
        {
            get;
            set;
        }

        public CalendarDay Wednesday
        {
            get;
            set;
        }

        public CalendarDay Thursday
        {
            get;
            set;
        }

        public CalendarDay Friday
        {
            get;
            set;
        }

        public CalendarDay Saturday
        {
            get;
            set;
        }

        public CalendarDay Sunday
        {
            get;
            set;
        }

        public int CurrentMonth
        {
            get;
            set;
        }

        public bool Equals(WeekWrapper other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Monday.Date.Equals(other.Monday.Date) &&
                   Tuesday.Date.Equals(other.Tuesday.Date) &&
                   Wednesday.Date.Equals(other.Wednesday.Date) &&
                   Thursday.Date.Equals(other.Thursday.Date) &&
                   Friday.Date.Equals(other.Friday.Date) &&
                   Saturday.Date.Equals(other.Saturday.Date) &&
                   Sunday.Date.Equals(other.Sunday.Date);
        }

        public override int GetHashCode()
        {
            var hashMonday = Monday == null ? 0 : Monday.Date.GetHashCode();
            var hashTuesday = Tuesday == null ? 0 : Tuesday.Date.GetHashCode();
            var hashWednesday = Wednesday == null ? 0 : Wednesday.Date.GetHashCode();
            var hashThursday = Thursday == null ? 0 : Thursday.Date.GetHashCode();
            var hashFriday = Friday == null ? 0 : Friday.Date.GetHashCode();
            var hashSaturday = Saturday == null ? 0 : Saturday.Date.GetHashCode();
            var hashSunday = Sunday == null ? 0 : Sunday.Date.GetHashCode();

            return hashMonday ^
                   hashTuesday ^
                   hashWednesday ^
                   hashThursday ^
                   hashFriday ^
                   hashSaturday ^
                   hashSunday;
        }

        public List<CalendarDay> GetStateHolidays()
        {
            var holidays = GetDaysByType(DayType.Holiday);
            return holidays.Where(calendarDay => calendarDay.IsStateHoliday).ToList();
        }

        public List<CalendarDay> GetHolidays()
        {
            return GetDaysByType(DayType.Holiday);
        }

        public List<CalendarDay> GetPreholidays()
        {
            return GetDaysByType(DayType.PreHoliday);
        }

        public List<CalendarDay> GetWorkdays()
        {
            return GetDaysByType(DayType.Workday);
        }

        private List<CalendarDay> GetDaysByType(DayType dayType)
        {
            var days = new List<CalendarDay>();

            if (Monday.Type == dayType) days.Add(Monday);
            if (Tuesday.Type == dayType) days.Add(Tuesday);
            if (Wednesday.Type == dayType) days.Add(Wednesday);
            if (Thursday.Type == dayType) days.Add(Thursday);
            if (Friday.Type == dayType) days.Add(Friday);

            if (Saturday.Type == dayType) days.Add(Saturday);
            if (Sunday.Type == dayType) days.Add(Sunday);

            return days;
        }

        public void SetStartTimeForWorkday(TimeSpan time)
        {
            foreach (var work in GetWorkdays())
                work.Start = time;
        }

        public void SetEndTimeForWorkday(TimeSpan time)
        {
            foreach (var work in GetWorkdays())
                work.End = time;
        }

        public void SetStartTimeForPreholiday(TimeSpan time)
        {
            foreach (var work in GetPreholidays())
                work.Start = time;
        }

        public void SetEndTimeForPreholiday(TimeSpan time)
        {
            foreach (var work in GetPreholidays())
                work.End = time;
        }
    }
}
