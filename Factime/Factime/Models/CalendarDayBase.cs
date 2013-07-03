using System;
using UseAbilities.MVVM.Base;

namespace Factime.Models
{
    public abstract class CalendarDayBase : ObserveProperty
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

        private DayType _type;
        public virtual DayType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged(()=> Type);
            }
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
                OnStateHolidayChanged(EventArgs.Empty);
            }
        }

        public event EventHandler StateHolidayChanged;

        public void OnStateHolidayChanged(EventArgs e)
        {
            var handler = StateHolidayChanged;
            if (handler != null) handler(this, e);
        }
    }
}
