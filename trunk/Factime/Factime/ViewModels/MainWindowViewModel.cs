using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Factime.Models;
using UseAbilities.Extensions.DateTimeExt;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.MVVM.Base;

namespace Factime.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _selectedMonth = DateTime.Now.Month;
            var weekCollection = new List<WeekWrapper>();
            
            for (var i = 1; i <= 12; i++)
            {
                var date = new DateTime(DateTime.Now.Year, i, 1);
                var weeks = date.GetWeeksAndDaysOfMonth();

                weekCollection.AddRange(weeks.Select(week => WrapCalendarDays(week, i)));
            }

            var distincWeekCollection = weekCollection.Distinct().ToList();

            UpdateWeekCollections(distincWeekCollection);
        }

        private static WeekWrapper WrapCalendarDays(IList<DateTime> week, int currentMonth)
        {
            var weekWrapper = new WeekWrapper(currentMonth);

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

        //private ObservableCollection<WeekWrapper> _weekCollections;
        //public ObservableCollection<WeekWrapper> WeekCollections
        //{
        //    get
        //    {
        //        return _weekCollections;
        //    }
        //    set
        //    {
        //        _weekCollections = value;
        //        OnPropertyChanged(() => WeekCollections);
        //    }
        //}

        private int _selectedMonth;
        public int SelectedMonth
        {
            get
            {
                return _selectedMonth;
            }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged(() => SelectedMonth);
                UpdateFilterWeekCollections();
            }
        }

        private ICollectionView _weekCollection;
        public ICollectionView WeekCollection
        {
            get
            {
                return _weekCollection;
            }
            set
            {
                _weekCollection = value;
                OnPropertyChanged(() => WeekCollection);
            }
        }

        private void UpdateWeekCollections(List<WeekWrapper> weekCollection)
        {
            var view = CollectionViewSource.GetDefaultView(weekCollection);
            if (view == null) return;
            view.Filter = FilterPredicate;

            WeekCollection = view;
        }

        private void UpdateFilterWeekCollections()
        {
            WeekCollection.Filter = FilterPredicate;
        }

        
        private bool FilterPredicate(object item)
        {
            var weekWrapper = item as WeekWrapper;
            if (weekWrapper == null) return false;

            var date = new DateTime(DateTime.Now.Year, SelectedMonth, 1);
            var weeks = date.GetWeeksAndDaysOfMonth();
            var monthes = weeks.Select(week => week.Select(dateTime => dateTime.Month).GetFrequentlyValues(1).FirstOrDefault()).ToList();
            
            return monthes.GetFrequentlyValues(1).FirstOrDefault() == weekWrapper.CurrentMonth;
        }
    }
}
