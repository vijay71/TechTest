using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Models
{
    
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

        

        private DailyWeather() { }

        
    }
}
