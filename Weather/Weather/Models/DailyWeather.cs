using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Models
{
    /// <summary>
    /// Represents a record of a day's weather data
    /// </summary>
    public class DailyWeather : IDailyWeather
    {
        public static DailyWeather EmptyDailyWeather = new DailyWeather();

        public int DayOfMonth { get; }
        public int MaxTemp { get; }
        public int MinTemp { get; }

        public DailyWeather(int dayOfMonth, int minTemp, int maxTemp)
        {
            if (dayOfMonth < 1 || dayOfMonth > 31) throw new ArgumentException("Day of month must be greater than 1 and less than 31");
            if (maxTemp < minTemp) throw new ArgumentException("Max temp must be lower than min temp.");

            DayOfMonth = dayOfMonth;
            MaxTemp = maxTemp;
            MinTemp = minTemp;
        }

        // Private constructor used for empty object

        private DailyWeather() { }

        // Equality operations overridden for completeness. The intent is that
        // this application treats DailyWeather as a value object.

        public override bool Equals(Object obj)
        {
            return obj is DailyWeather dailyWeather && this == dailyWeather;
        }

        public override int GetHashCode()
        {
            return DayOfMonth.GetHashCode() ^ MaxTemp.GetHashCode() ^ MinTemp.GetHashCode();
        }

        public static bool operator ==(DailyWeather x, DailyWeather y)
        {
            return x.DayOfMonth == y.DayOfMonth && x.MaxTemp == y.MaxTemp && x.MinTemp == y.MinTemp;
        }

        public static bool operator !=(DailyWeather x, DailyWeather y)
        {
            return !(x == y);
        }
    }
}
