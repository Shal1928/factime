using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Factime.Models;
using UseAbilities.Extensions.DateTimeExt;
using UseAbilities.MVVM.Base;

namespace Factime.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var weeks = DateTime.Now.GetWeeksAndDaysOfMonth();
            var weekCollection = new ObservableCollection<WeekWrapper>();

            foreach (var week in weeks)
                weekCollection.Add(WrapCalendarDays(week));
            

            WeekCollections = weekCollection;
        }

        private WeekWrapper WrapCalendarDays(List<DateTime> week)
        {
            var weekWrapper = new WeekWrapper();

            var startTime = new TimeSpan(9, 00, 00);
            var endTime = new TimeSpan(18, 30, 00);

            weekWrapper.Monday = new CalendarDay
            {
                Date = week[0],
                Start = startTime,
                End = endTime,
                Type = DayType.Workday
            };
            weekWrapper.Tuesday = new CalendarDay
            {
                Date = week[1],
                Start = startTime,
                End = endTime,
                Type = DayType.Workday
            };
            weekWrapper.Wednesday = new CalendarDay
            {
                Date = week[2],
                Start = startTime,
                End = endTime,
                Type = DayType.Workday
            };
            weekWrapper.Thursday = new CalendarDay
            {
                Date = week[3],
                Start = startTime,
                End = endTime,
                Type = DayType.Workday
            };
            weekWrapper.Friday = new CalendarDay
            {
                Date = week[4],
                Start = startTime,
                End = endTime,
                Type = DayType.Workday
            };
            weekWrapper.Saturday = new CalendarDay
            {
                Date = week[5],
                Start = startTime,
                End = endTime,
                Type = DayType.Holiday
            };
            weekWrapper.Sunday = new CalendarDay
            {
                Date = week[6],
                Start = startTime,
                End = endTime,
                Type = DayType.Holiday
            };

            return weekWrapper;
        }

        private ObservableCollection<WeekWrapper> _weekCollections;
        public ObservableCollection<WeekWrapper> WeekCollections
        {
            get
            {
                return _weekCollections;
            }
            set
            {
                _weekCollections = value;
                OnPropertyChanged(() => WeekCollections);
            }
        }
    }
}
