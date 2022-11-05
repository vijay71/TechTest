using Weather.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Services
{
    public interface IDailyWeatherFactory
    {
        bool TryCreate(string dayOfMonth, string minTemp, string maxTemp, out IDailyWeather dailyWeather);
        bool TryCreate(int dayOfMonth, int minTemp, int maxTemp, out IDailyWeather dailyWeather);
    }
}
