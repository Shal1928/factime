using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
