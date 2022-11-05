using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Weather.Models
{
    /// <summary>
    /// Represents a record of a day's temperature data
    /// </summary>
    public class DailyTemperature : IntDifferentiable, IDailyTemperature
    {
        public static DailyTemperature EmptyDailyWeather = new DailyTemperature();

        public int DayOfMonth { get; }

        public DailyTemperature(int dayOfMonth, int minTemp, int maxTemp) : base(minTemp, maxTemp)
        {
            if (dayOfMonth < 1 || dayOfMonth > 31) throw new ArgumentException("Day of month must be greater than 1 and less than 31");
            if (maxTemp < minTemp) throw new ArgumentException("Max temp must be lower than min temp.");

            DayOfMonth = dayOfMonth;
        }

        // Private constructor used for empty object

        private DailyTemperature() : base(0,0) { }

        // Equality operations overridden for completeness. The intent is that
        // this application treats IntDifferentiable as a value object.

        public override bool Equals(Object obj)
        {
            return obj is DailyTemperature dailyWeather && this == dailyWeather;
        }

        public override int GetHashCode()
        {
            return DayOfMonth.GetHashCode() ^ AbsoluteDifference.GetHashCode();
        }

        public static bool operator ==(DailyTemperature x, DailyTemperature y)
        {
            return x.DayOfMonth == y.DayOfMonth && x.AbsoluteDifference == y.AbsoluteDifference;
        }

        public static bool operator !=(DailyTemperature x, DailyTemperature y)
        {
            return !(x == y);
        }
    }
}
