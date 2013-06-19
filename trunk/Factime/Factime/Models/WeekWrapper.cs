using System;

namespace Factime.Models
{
    public class WeekWrapper : IEquatable<WeekWrapper>
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

        public bool Equals(WeekWrapper other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Monday.Date.Equals(other.Monday.Date) &&
                   Tuesday.Date.Equals(other.Tuesday.Date) &&
                   Wednesday.Date.Equals(other.Wednesday.Date) &&
                   Thursday.Date.Equals(other.Thursday.Date) &&
                   Friday.Date.Equals(other.Friday.Date) &&
                   Saturday.Date.Equals(other.Saturday.Date) &&
                   Sunday.Date.Equals(other.Sunday.Date);
        }

        public override int GetHashCode()
        {
            var hashMonday = Monday == null ? 0 : Monday.Date.GetHashCode();
            var hashTuesday = Tuesday == null ? 0 : Tuesday.Date.GetHashCode();
            var hashWednesday = Wednesday == null ? 0 : Wednesday.Date.GetHashCode();
            var hashThursday = Thursday == null ? 0 : Thursday.Date.GetHashCode();
            var hashFriday = Friday == null ? 0 : Friday.Date.GetHashCode();
            var hashSaturday = Saturday == null ? 0 : Saturday.Date.GetHashCode();
            var hashSunday = Sunday == null ? 0 : Sunday.Date.GetHashCode();

            return hashMonday ^
                   hashTuesday ^
                   hashWednesday ^
                   hashThursday ^
                   hashFriday ^
                   hashSaturday ^
                   hashSunday;
        }
    }
}
