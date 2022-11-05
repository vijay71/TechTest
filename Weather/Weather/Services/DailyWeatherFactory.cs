using Weather.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Services
{
   
    public class DailyWeatherFactory : IDailyWeatherFactory   {
       
        

        
        public bool TryCreate(string dayOfMonth, string minTemp, string maxTemp, out IDailyWeather dailyWeather)
        {
            bool successfulParse;
            try
            {
                successfulParse = int.TryParse(dayOfMonth, out int parsedDay);
                successfulParse &= int.TryParse(minTemp, out int parsedMinTemp);
                successfulParse &= int.TryParse(maxTemp, out int parsedMaxTemp);

             

                if (successfulParse)
                    TryCreate(parsedDay, parsedMinTemp, parsedMaxTemp, out dailyWeather);
                else
                    dailyWeather = DailyWeather.EmptyDailyWeather;
            }
            catch(Exception ex)
            {
             
                dailyWeather = DailyWeather.EmptyDailyWeather;
            }
            return (DailyWeather)dailyWeather != DailyWeather.EmptyDailyWeather;
        }

      
        public bool TryCreate(int dayOfMonth, int minTemp, int maxTemp, out IDailyWeather dailyWeather)
        {
            try
            {
                dailyWeather = new DailyWeather(dayOfMonth, minTemp, maxTemp);
            }
            catch(Exception ex)
            {
                
                dailyWeather = DailyWeather.EmptyDailyWeather;
            }

            return (DailyWeather)dailyWeather != DailyWeather.EmptyDailyWeather;
        }
    }
}
