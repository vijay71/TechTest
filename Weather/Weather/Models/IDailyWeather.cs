using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Models
{
    public interface IDailyWeather
    {
        int DayOfMonth { get; }
        int MaxTemp { get; }
        int MinTemp { get; }
    }
}
