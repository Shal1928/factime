﻿using System;
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


        #region Public Properties

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

            return Monday.Date.Equals(other.Monday.Date) && //Monday.IsStateHoliday.Equals(other.Monday.IsStateHoliday) &&
                   Tuesday.Date.Equals(other.Tuesday.Date) && //Tuesday.IsStateHoliday.Equals(other.Tuesday.IsStateHoliday) &&
                   Wednesday.Date.Equals(other.Wednesday.Date) && //Wednesday.IsStateHoliday.Equals(other.Wednesday.IsStateHoliday) &&
                   Thursday.Date.Equals(other.Thursday.Date) && //Thursday.IsStateHoliday.Equals(other.Thursday.IsStateHoliday) &&
                   Friday.Date.Equals(other.Friday.Date) && //Friday.IsStateHoliday.Equals(other.Friday.IsStateHoliday) &&
                   Saturday.Date.Equals(other.Saturday.Date) && //Saturday.IsStateHoliday.Equals(other.Saturday.IsStateHoliday) &&
                   Sunday.Date.Equals(other.Sunday.Date);// && Sunday.IsStateHoliday.Equals(other.Sunday.IsStateHoliday);
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

        public List<CalendarDay> GetPreholidays()
        {
            return GetDaysByType(DayType.PreHoliday);
        }

        public List<CalendarDay> GetWorkdays()
        {
            return GetDaysByType(DayType.Workday);
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

        public void SetPreHolidays()
        {
            //foreach (var calendarDay in GetAllDaysCollection<CalendarDay>())
            //{
            //    if (!calendarDay.IsStateHoliday) continue;

            //    var cDay = GetDayByDate(calendarDay.Date.AddDays(-1));
            //    if (cDay != null && cDay.Type != DayType.Holiday) cDay.Type = DayType.PreHoliday;
            //}
            foreach (var cDay in
                              from calendarDay in GetAllDaysCollection<CalendarDay>()
                              where calendarDay.IsStateHoliday
                              select GetDayByDate(calendarDay.Date.AddDays(-1))
                                  into cDay
                                  where cDay != null && cDay.Type != DayType.Holiday
                                  select cDay)
                cDay.Type = DayType.PreHoliday;

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