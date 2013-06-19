﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Factime.Models;
using Factime.Stores;
using UseAbilities.Extensions.DateTimeExt;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.IoC.Attributes;
using UseAbilities.IoC.Stores;
using UseAbilities.MVVM.Base;
using UseAbilities.MVVM.Command;

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

        private WeekWrapper WrapCalendarDays(IList<DateTime> week, int currentMonth)
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

        [InjectedProperty]
        public IXmlStore<FactimeSettings> FactimeSettingsStore
        {
            get; 
            set;
        }

        private FactimeSettings _factimeSettings;
        public FactimeSettings FactimeSettings
        {
            get
            {
                return _factimeSettings ?? (_factimeSettings = FactimeSettingsStore.Load());
            }
        }

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

        private bool _selectStateHoliday;
        public bool SelectStateHoliday
        {
            get
            {
                return _selectStateHoliday;
            }
            set
            {
                _selectStateHoliday = value;
                OnPropertyChanged(() => SelectStateHoliday);
            }
        }

        #region Start & End Time Properties

        public TimeSpan WorkdayStartTime
        {
            get
            {
                return FactimeSettings.DefaultStart;
            }
            set
            {
                FactimeSettings.DefaultStart = value;
                OnPropertyChanged(() => WorkdayStartTime);
                UpdateWorkTime(DayType.Workday, value, true);
            }
        }

        public TimeSpan WorkdayEndTime
        {
            get
            {
                return FactimeSettings.DefaultEnd;
            }
            set
            {
                FactimeSettings.DefaultEnd = value;
                OnPropertyChanged(() => WorkdayEndTime);
                UpdateWorkTime(DayType.Workday, value, false);
            }
        }

        public TimeSpan PreholidayStartTime
        {
            get
            {
                return FactimeSettings.ShortStart;
            }
            set
            {
                FactimeSettings.ShortStart = value;
                OnPropertyChanged(() => PreholidayStartTime);
                UpdateWorkTime(DayType.PreHoliday, value, true);
            }
        }

        public TimeSpan PreholidayEndTime
        {
            get
            {
                return FactimeSettings.ShortEnd;
            }
            set
            {
                FactimeSettings.ShortEnd = value;
                OnPropertyChanged(() => PreholidayEndTime);
                UpdateWorkTime(DayType.PreHoliday, value, false);
            }
        }

        #endregion

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

        private void UpdateWorkTime(DayType dayType, TimeSpan time, bool isStartTime)
        {
            //Update StartTime
            if(isStartTime)
            {
                //Update Workday
                if(dayType == DayType.Workday)
                    foreach (WeekWrapper weekWrapper in WeekCollection)
                        weekWrapper.SetStartTimeForWorkday(time);

                //Update PreHoliday
                else
                    foreach (WeekWrapper weekWrapper in WeekCollection)
                        weekWrapper.SetStartTimeForPreholiday(time);
            }
            //Update EndTime
            else
            {
                //Update Workday
                if (dayType == DayType.Workday)
                    foreach (WeekWrapper weekWrapper in WeekCollection)
                        weekWrapper.SetEndTimeForWorkday(time);

                //Update PreHoliday
                else
                    foreach (WeekWrapper weekWrapper in WeekCollection)
                        weekWrapper.SetEndTimeForPreholiday(time);
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
            return weeks.Select(week => WrapCalendarDays(week, SelectedMonth)).Any(weekWrapper.Equals);
        }

        #region Commands

        private ICommand _saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ?? (_saveSettingsCommand = new RelayCommand(param => OnSaveSettingsCommand(), null));
            }
        }

        private void OnSaveSettingsCommand()
        {
            foreach (WeekWrapper week in WeekCollection)
                FactimeSettings.DefaultHolidaysCollection.AddRange(week.GetStateHolidays().Select(day=> day.Date));
            
            FactimeSettingsStore.Save(FactimeSettings);
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (_loadedCommand = new RelayCommand(param => OnLoadedCommand(), null));
            }
        }

        private void OnLoadedCommand()
        {
            var stateHolidays = FactimeSettings.DefaultHolidaysCollection;
        }

        

        #endregion

        
    }
}
