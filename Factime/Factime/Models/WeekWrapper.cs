namespace Factime.Models
{
    public class WeekWrapper
    {
        public WeekWrapper(int curentMonth)
        {
            CurrentMonth = curentMonth;
        }

        public CalendarDay Monday
        {
            get; 
            set;
        }

        public CalendarDay Tuesday
        {
            get;
            set;
        }

        public CalendarDay Wednesday
        {
            get;
            set;
        }

        public CalendarDay Thursday
        {
            get;
            set;
        }

        public CalendarDay Friday
        {
            get;
            set;
        }

        public CalendarDay Saturday
        {
            get;
            set;
        }

        public CalendarDay Sunday
        {
            get;
            set;
        }

        public int CurrentMonth
        {
            get;
            set;
        }
    }
}
