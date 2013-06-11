using System;
using System.Collections.Generic;
using System.IO;
using Factime.Models;
using UseAbilities.IoC.Attributes;
using UseAbilities.IoC.Stores;

namespace Factime.Stores
{
    public class CalendarDayStore : FileStore<List<CalendarDay>>
    {
        public CalendarDayStore()
        {
            Owerwrite = true;
        }

        [InjectedProperty]
        public IXmlStore<FactimeSettings> FactimeSettingsStore
        {
            get;
            set;
        }

        public override void Save(List<CalendarDay> storeObject)
        {
            try
            {
                Directory.CreateDirectory(Directory.GetParent(FileName).FullName);

                //27.05.2014 08:30 18:30
                using (var file = new StreamWriter(FileName, !Owerwrite))
                {
                    foreach (var calendarDay in storeObject)
                        file.WriteLine("{1}{0}{2}{0}{3}", Splitter, calendarDay.Date, calendarDay.Start, calendarDay.End);
                }
            }
            catch (Exception e)
            { 
                throw e;
            }

            
        }

        public override List<CalendarDay> Load()
        {
            if (File.Exists(FileName)) throw new FileNotFoundException(FileName);

            var calendarDayCollection = new List<CalendarDay>();

            try
            {
                //27.05.2014 08:30 18:30
                using (var file = new StreamReader(FileName))
                {
                    while (!file.EndOfStream)  // framework 2.0
                    {
                        var readLine = file.ReadLine();
                        if (readLine == null) throw new Exception("readLine is null!");

                        var factimeSettings = FactimeSettingsStore.Load();
                        var calendarDayData = readLine.Split(Splitter);
                        var date = Convert.ToDateTime(calendarDayData[0]);
                        var start = TimeSpan.Parse(calendarDayData[1]);
                        var end = TimeSpan.Parse(calendarDayData[2]);
                        var dayType = end == factimeSettings.DefaultEnd ? DayType.Workday : DayType.PreHoliday;


                        calendarDayCollection.Add(new CalendarDay
                                                      {
                                                            Date = date,
                                                            Start = start,
                                                            End = end,
                                                            Type = dayType
                                                      });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return calendarDayCollection;
        }

        private char _splitter = ' ';
        public char Splitter
        {
            get { return _splitter; }
            set { _splitter = value; }
        }

        public bool Owerwrite
        {
            get; 
            set;
        }
    }
}
