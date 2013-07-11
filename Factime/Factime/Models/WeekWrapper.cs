using System;
using System.Collections.Generic;
using System.Linq;
using UseAbilities.DotNet.EventArguments;

namespace Factime.Models
{
    public class WeekWrapper : IEquatable<WeekWrapper>
    {
        public WeekWrapper(int curentMonth)
        {
            CurrentMonth = curentMonth;           
        }


        #region Public Properties

        private CalendarDay _monday;
        public CalendarDay Monday
        {
            get
            {
                return _monday;
            }
            set
            {
                _monday = value;
                _monday.StateHolidayChanged += (sender, e) => SetPreHolidays(e);
            }
        }

        private CalendarDay _tuesday;
        public CalendarDay Tuesday
        {
            get
            {
                return _tuesday;
            }
            set
            {
                _tuesday = value;
                _tuesday.StateHolidayChanged += (sender, e) => SetPreHolidays(e);
            }
        }

        private CalendarDay _wednesday;
        public CalendarDay Wednesday
        {
            get
            {
                return _wednesday;
            }
            set
            {
                _wednesday = value;
                _wednesday.StateHolidayChanged += (sender, e) => SetPreHolidays(e);
            }
        }

        private CalendarDay _thursday;
        public CalendarDay Thursday
        {
            get
            {
                return _thursday;
            }
            set
            {
                _thursday = value;
                _thursday.StateHolidayChanged += (sender, e) => SetPreHolidays(e);
            }
        }

        private CalendarDay _friday;
        public CalendarDay Friday
        {
            get
            {
                return _friday;
            }
            set
            {
                _friday = value;
                _friday.StateHolidayChanged += (sender, e) => SetPreHolidays(e);
            }
        }

        private CalendarDay _saturday;
        public CalendarDay Saturday
        {
            get
            {
                return _saturday;
            }
            set
            {
                _saturday = value;
                _saturday.StateHolidayChanged += (sender, e) => SetPreHolidays(e);
            }
        }

        private CalendarDay _sunday;
        public CalendarDay Sunday
        {
            get
            {
                return _sunday;
            }
            set
            {
                _sunday = value;
                _sunday.StateHolidayChanged += (sender, e) => SetPreHolidays(e);
            }
        }

        public int CurrentMonth
        {
            get;
            set;
        }

        #endregion


        #region IEquatable Implementation

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

        #endregion


        #region Public Methods

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

        #endregion
        

        #region Get Day/s Public Methods

        //TODO: Resolve this Magic!
        public List<T> GetAllDaysCollection<T>()
        {
            return typeof(T) == typeof(DateTime) ? new List<T> { (dynamic)Monday.Date, (dynamic)Tuesday.Date, (dynamic)Wednesday.Date, (dynamic)Thursday.Date, (dynamic)Friday.Date, (dynamic)Saturday.Date, (dynamic)Sunday.Date } :
                                                   new List<T> { (dynamic)Monday, (dynamic)Tuesday, (dynamic)Wednesday, (dynamic)Thursday, (dynamic)Friday, (dynamic)Saturday, (dynamic)Sunday };
        }

        public CalendarDay GetDayByDate(DateTime date)
        {
            return GetAllDaysCollection<CalendarDay>().Where(cDay => cDay.Date == date).FirstOrDefault();
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

        public List<CalendarDay> GetHolidays(int year)
        {
            return GetDaysByType(DayType.Holiday).Where(d => d.Date.Year == year).ToList();
        }

        public List<CalendarDay> GetPreholidays()
        {
            return GetDaysByType(DayType.PreHoliday);
        }

        public List<CalendarDay> GetPreholidays(int year)
        {
            return GetDaysByType(DayType.PreHoliday).Where(d => d.Date.Year == year).ToList();
        }

        public List<CalendarDay> GetWorkdays()
        {
            return GetDaysByType(DayType.Workday);
        }

        public List<CalendarDay> GetWorkdays(int year)
        {
            return GetDaysByType(DayType.Workday).Where(d => d.Date.Year == year).ToList();
        }

        public List<CalendarDay> GetExportDays()
        {
            var days = new List<CalendarDay>();
            days.AddRange(GetAllDaysCollection<CalendarDay>().Where(calendarDay => calendarDay.Type == DayType.Workday || calendarDay.Type == DayType.PreHoliday));

            return days;
        }

        private List<CalendarDay> GetDaysByType(DayType dayType)
        {
            var days = new List<CalendarDay>();
            days.AddRange(GetAllDaysCollection<CalendarDay>().Where(calendarDay => calendarDay.Type == dayType));

            return days;
        }

        #endregion
        

        #region Set Day/s Public Methods

        public void SetStateHolidays(List<DateTime> stateHolidaysDateCollection)
        {
            foreach (var stateHolidayDate in stateHolidaysDateCollection)
            {
                foreach (var calendarDay in GetAllDaysCollection<CalendarDay>())
                    if (calendarDay.Date.Day == stateHolidayDate.Day && calendarDay.Date.Month == stateHolidayDate.Month) calendarDay.IsStateHoliday = true;
            }

            SetPreHolidays();
        }

        public void SetPreHolidays(EventArgs e = null)
        {
            var targetDayType = DayType.PreHoliday;
            var stateHolidayPredicate = true;

            var valueChangedEventArgs = e as ValueChangedEventArgs;
            if (valueChangedEventArgs != null && (bool)valueChangedEventArgs.OldValue && !(bool)valueChangedEventArgs.NewValue)
            {
                stateHolidayPredicate = false;
                targetDayType = DayType.Workday;
            }
                
            //foreach (var calendarDay in GetAllDaysCollection<CalendarDay>())
            //{
            //    if (calendarDay.IsStateHoliday != stateHolidayPredicate) continue;

            //    var cDay = GetDayByDate(calendarDay.Date.AddDays(-1));
            //    if (cDay != null && cDay.Type != DayType.Holiday) cDay.Type = targetDayType;
            //}

            foreach (var cDay in
                              from calendarDay in GetAllDaysCollection<CalendarDay>()
                              where calendarDay.IsStateHoliday == stateHolidayPredicate
                              select GetDayByDate(calendarDay.Date.AddDays(-1))
                                  into cDay
                                  where cDay != null && cDay.Type != DayType.Holiday
                                  select cDay)
                cDay.Type = targetDayType;

        }

        public void SetDay(CalendarDay calendarDay)
        {
            var cDay = GetDayByDate(calendarDay.Date);
            cDay.Type = calendarDay.Type;
            cDay.Start = calendarDay.Start;
            cDay.End = calendarDay.End;

            if (cDay.Type == DayType.Holiday) SetPreHolidays();
        }

        #endregion
        

        #region Set Time Public Methods

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
            foreach (var preholiday in GetPreholidays())
                preholiday.Start = time;
        }

        public void SetEndTimeForPreholiday(TimeSpan time)
        {
            foreach (var preholiday in GetPreholidays())
                preholiday.End = time;
        }

        #endregion

        
        //public static explicit operator WeekWrapper(List<CalendarDay> calendarDayCollection)
        //{
        //    return new WeekWrapper();
        //}

        //public static explicit operator WeekWrapper(List<CalendarDay> calendarDayCollection)
        //{
        //    return new WeekWrapper();
        //}
    }
}
