using System;
using System.Collections.Generic;
using System.Linq;

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

        public static explicit operator DateTime(CalendarDay calendarDay)
        {
            return calendarDay.Date;
        }

        public static List<DateTime> ToList(List<CalendarDay> calendarDayCollection)
        {
            return calendarDayCollection.Select(cDay => cDay.Date).ToList();
        }

        public static List<CalendarDay> ToList(List<DateTime> dateTimeCollection)
        {
            return dateTimeCollection.Select(dateTime => new CalendarDay { Date = dateTime }).ToList();
        }
    }

    public static class CalendarDayExt
    {
        public static List<CalendarDay> ToList(List<DateTime> dateTimeCollection)
        {
            return dateTimeCollection.Select(dateTime => new CalendarDay {Date = dateTime}).ToList();
        }

        public static List<CalendarDay> SetHolidayType(this List<CalendarDay> dateTimeCollection)
        {
            foreach (var calendarDay in dateTimeCollection)
                calendarDay.Type = DayType.Holiday;   

            return dateTimeCollection;
        }
    }

    //    a.cs

    //class zzz

    //{

    //public static void Main()

    //{

    //zzz a = new zzz();

    //yyy b = new yyy();

    //xxx c = new xxx();

    //aaa d = new aaa();

    //a.abc(b);

    //System.Console.WriteLine();

    //}

    //public void abc(xxx x)
    //{

    //System.Console.WriteLine("abc xxx");

    //}

    //public void abc(aaa x)
    //{

    //System.Console.WriteLine("abc aaa");

    //}

    //}

    //class yyy//CalendarDay
    //{
    //    DateTime                    xxx=DateTime //yyy=CalendarDay
    //    public static explicit operator DateTime(yyy a)
    //    {
    //        System.Console.WriteLine("op xxx");
    //        DateTime
    //        return new DateTime();
    //    }

    //    public static implicit operator aaa( yyy a)
    //    {
    //        System.Console.WriteLine("op aaa");
    //        return new aaa();
    //    }
    //}

    //class xxx
    //{

    //}

    //class aaa 
    //{

    //}

 

    //Output

    //op aaa

    //abc aaa

    //public static DateTime ToDateTime(CalendarDay  obj)
    //{
    //  return obj.Date;
    //}
}
