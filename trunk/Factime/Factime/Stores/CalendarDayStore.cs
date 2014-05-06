using System;
using System.Collections.Generic;
using System.IO;
using Factime.Models;
using UseAbilities.IoC.Attributes;
using UseAbilities.IoC.Stores;

namespace Factime.Stores
{
    public class CalendarDayStore : AbstractFileStore<List<CalendarDay>>
    {
        #region Singleton implementation

        private CalendarDayStore()
        {
            Owerwrite = true;
        }

        private static readonly CalendarDayStore SingleInstance = new CalendarDayStore();
        public static CalendarDayStore Instance { get { return SingleInstance; } }

        #endregion

        [InjectedProperty]
        public IFileStore<FactimeSettings> FactimeSettingsStore { get; set; }

        public override void Save(List<CalendarDay> storeObject)
        {
            Save(storeObject, GetFileName());
        }

        public override void Save(List<CalendarDay> storeObject, string key)
        {
            Directory.CreateDirectory(Directory.GetParent(key).FullName);
            //27.05.2014 08:30 18:30
            using (var file = new StreamWriter(key, !Owerwrite))
            {
                foreach (var calendarDay in storeObject)
                    file.WriteLine("{1}{0}{2}{0}{3}", Splitter, calendarDay.Date.ToString("dd.MM.yyy"), calendarDay.Start.ToString().Remove(5), calendarDay.End.ToString().Remove(5));
            }
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

        public override List<CalendarDay> Load()
        {
            return Load(GetFileName());
        }

        public override List<CalendarDay> Load(string key)
        {
            if (!File.Exists(key)) return null; //throw new FileNotFoundException(FileName);

            var calendarDayCollection = new List<CalendarDay>();

            //27.05.2014 08:30 18:30
            using (var file = new StreamReader(key))
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

            return calendarDayCollection;
        }
    }
}
