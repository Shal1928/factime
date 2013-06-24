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
        private bool _isFilterNull = true;
        private bool _isImporting;

        public MainWindowViewModel()
        {
            _selectedMonth = DateTime.Now.Month;
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

            if (CalendarDayCollection != null && _isImporting)
            {
                var weeks = CalendarDay.ToList(CalendarDayCollection);//.SeparateByWeeks();
                
                //foreach (var calendarDay in CalendarDayCollection)
                //{
                //    foreach (var weekWrap in weekWrapper.GetAllDaysCollection<CalendarDay>())
                //        if (calendarDay.Date.Equals(weekWrap)) weekWrap.Fill(calendarDay);

                //    //if (calendarDay.Date.ThisDateBelongsToThisWeek(weekWrapper.GetAllDaysCollection<DateTime>())) 
                //    //    weekWrapper.GetDayByDate(calendarDay.Date).Fill(calendarDay);

                    

                //    //foreach (var daysOfWeek in weeks)
                //    //{
                //    //    _holidayCollection.AddRange(weekWrapper.GetAllDaysCollection<DateTime>().FindMissing<DateTime>(daysOfWeek).ToList());
                //    //}

                //}
                //if (_holidayCollection.Count >= 142) 
                //    System.Diagnostics.Debugger.Break();

                var holidayCollection = new List<DateTime>();
                holidayCollection.AddRange(weekWrapper.GetAllDaysCollection<DateTime>().FindMissing<DateTime>(weeks).ToList());


                //foreach (var calendarDay in CalendarDay.ToList(holidayCollection).SetHolidayType())
                //{
                //    foreach (var weekWrap in weekWrapper.GetAllDaysCollection<CalendarDay>())
                //        if (calendarDay.Date.Equals(weekWrap.Date)) 
                //            weekWrap.Fill(calendarDay);
                //}

                foreach (var weekWrap in weekWrapper.GetAllDaysCollection<CalendarDay>())
                {
                    foreach (var calendarDay in CalendarDay.ToList(holidayCollection).SetHolidayType())
                    {
                            if (calendarDay.Date.Equals(weekWrap.Date))
                                    weekWrapper.SetDay(calendarDay);
                                    //weekWrap.Fill(calendarDay);
                    }
                }

                
            }
            
            return weekWrapper;
        }

        //private List<DateTime> _holidayCollection = new List<DateTime>();

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

        #region InjectedProperty

        [InjectedProperty]
        public IXmlStore<FactimeSettings> FactimeSettingsStore
        {
            get; 
            set;
        }

        [InjectedProperty]
        public IFileStore<List<CalendarDay>> CalendarDayStore
        {
            get;
            set;
        }

        #endregion

        private FactimeSettings _factimeSettings;
        public FactimeSettings FactimeSettings
        {
            get
            {
                return _factimeSettings ?? (_factimeSettings = FactimeSettingsStore.Load());
            }
        }

        private List<CalendarDay> _calendarDayCollection;
        public List<CalendarDay> CalendarDayCollection
        {
            get
            {
                return _calendarDayCollection ?? (_calendarDayCollection = CalendarDayStore.Load());
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
                _isFilterNull = false;
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

        public string OutputPath
        {
            get
            {
                return FactimeSettings.OutputPath;
            }
            set
            {
                FactimeSettings.OutputPath = value;
                OnPropertyChanged(() => OutputPath);
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

        private void UpdateWorkTime()
        {
            UpdateWorkTime(DayType.Workday, WorkdayStartTime, true);
            UpdateWorkTime(DayType.Workday, WorkdayEndTime, false);
            UpdateWorkTime(DayType.PreHoliday, PreholidayStartTime, true);
            UpdateWorkTime(DayType.PreHoliday, PreholidayEndTime, false);
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
            if (_isFilterNull) WeekCollection.Filter = null;
            else WeekCollection.Filter = FilterPredicate;
        }
     
        private bool FilterPredicate(object item)
        {
            var weekWrapper = item as WeekWrapper;
            if (weekWrapper == null) return false;

            var date = new DateTime(DateTime.Now.Year, SelectedMonth, 1);
            var weeks = date.GetWeeksAndDaysOfMonth();
            _isImporting = false;
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
            FactimeSettings.DefaultHolidaysCollection.Clear();

            var filter = WeekCollection.Filter;
            WeekCollection.Filter = null;

            foreach (WeekWrapper week in WeekCollection)
                FactimeSettings.DefaultHolidaysCollection.AddRange(week.GetStateHolidays().Select(day=> day.Date));
            
            FactimeSettingsStore.Save(FactimeSettings);

            WeekCollection.Filter = filter;
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
            var weekCollection = new List<WeekWrapper>();

            for (var i = 1; i <= 12; i++)
            {
                var date = new DateTime(DateTime.Now.Year, i, 1);
                var weeks = date.GetWeeksAndDaysOfMonth();

                weekCollection.AddRange(weeks.Select(week => WrapCalendarDays(week, i)));
            }

            var distincWeekCollection = weekCollection.Distinct().ToList();

            foreach (var week in distincWeekCollection)
                week.SetStateHolidays(FactimeSettings.DefaultHolidaysCollection);

            UpdateWeekCollections(distincWeekCollection);
        }

        private ICommand _importCommand;
        public ICommand ImportCommand
        {
            get
            {
                return _importCommand ?? (_importCommand = new RelayCommand(param => OnImportCommand(), null));
            }
        }

        private void OnImportCommand()
        {
            UpdateWorkTime();
            //_holidayCollection.Clear();
            CalendarDayStore.FileName = OutputPath;
            _isFilterNull = false;
            _isImporting = true;
            OnLoadedCommand();
            _isFilterNull = true;
            _isImporting = false;
            //foreach (var holiday in _holidayCollection)
            //    Console.WriteLine(holiday);
        }


        private ICommand _exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                return _exportCommand ?? (_exportCommand = new RelayCommand(param => OnExportCommand(), null));
            }
        }

        private void OnExportCommand()
        {
            var filter = WeekCollection.Filter;
            WeekCollection.Filter = null;

            UpdateWorkTime();
            var exportCalendarDayCollection = new List<CalendarDay>();
            
            //TODO: Send to Action from WeekWrapper
            foreach (WeekWrapper week in WeekCollection)
                exportCalendarDayCollection.AddRange(week.GetExportDays());

            CalendarDayStore.FileName = OutputPath;
            //UpdateTimeTemp(ref exportCalendarDayCollection, WorkdayStartTime, WorkdayEndTime);
            CalendarDayStore.Save(exportCalendarDayCollection);

            WeekCollection.Filter = filter;
        }

        private void UpdateTimeTemp(ref List<CalendarDay> calendarDayCollection, TimeSpan startTime, TimeSpan endTime)
        {
            foreach (var calendarDay in calendarDayCollection)
                if(calendarDay.Type == DayType.Workday)
                {
                    calendarDay.Start = startTime;
                    calendarDay.End = endTime;
                }
                else
                {
                    calendarDay.Start = startTime;
                    calendarDay.End = endTime - new TimeSpan(1, 0, 0);
                }
        }

        #endregion

        
    }
}
