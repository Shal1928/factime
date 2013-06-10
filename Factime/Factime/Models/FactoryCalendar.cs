using System;
using System.Collections.Generic;
using System.Linq;

namespace Factime.Models
{
    //TODO: Remove
    public class FactoryCalendar
    {
        public int Year
        {
            get
            {
                if (CalendarDayCollection != null)
                {
                    var firstOrDefault = CalendarDayCollection.FirstOrDefault();
                    if (firstOrDefault != null) return firstOrDefault.Date.Year;
                }

                throw new ArgumentException("CalendarDayCollection is null or FirstOrDefault value is null!");
            }
        }

        public List<CalendarDay> CalendarDayCollection
        {
            get; 
            set;
        }
    }
}
