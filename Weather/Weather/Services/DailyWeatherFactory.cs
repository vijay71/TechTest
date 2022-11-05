using Weather.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Services
{
    /// <summary>
    /// Encapsulates the logic needed to create a Daily Weather object.
    /// </summary>
    public class DailyWeatherFactory : IDailyWeatherFactory
    {
        private readonly ILoggingService _loggingService;

        public DailyWeatherFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Attempts to create a DailyWeather object.
        /// </summary>
        /// <param name="dayOfMonth">String representing the day of the month the weather was recorded. Should be a numeric value.</param>
        /// <param name="minTemp">String representing the min temperature for the day. Should be a numeric value.</param>
        /// <param name="maxTemp">String representing the max temperature for the day. Should be a numeric value.</param>
        /// <param name="dailyWeather">A DailyWeather object created from the input values (or an empty implementation if parsing failed).</param>
        /// <returns>True if object successfully created with input params. False if not and out param is set to empty object.</returns>
        public bool TryCreate(string dayOfMonth, string minTemp, string maxTemp, out IDailyWeather dailyWeather)
        {
            bool successfulParse;
            try
            {
                successfulParse = int.TryParse(dayOfMonth, out int parsedDay);
                successfulParse &= int.TryParse(minTemp, out int parsedMinTemp);
                successfulParse &= int.TryParse(maxTemp, out int parsedMaxTemp);

                // Could throw an error to indicate there was an issue parsing with specific detail on what failed;
                // Because this application does not have specific error handling reqs, I like using the empty object 
                // here for simplicity.

                if (successfulParse)
                    TryCreate(parsedDay, parsedMinTemp, parsedMaxTemp, out dailyWeather);
                else
                    dailyWeather = DailyWeather.EmptyDailyWeather;
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to create DailyWeather object from string values.", ex);
                dailyWeather = DailyWeather.EmptyDailyWeather;
            }
            return (DailyWeather)dailyWeather != DailyWeather.EmptyDailyWeather;
        }

        /// <summary>
        /// Creates a DailyWeather object.
        /// </summary>
        /// <returns>True if object successfully created with input params. False if not and out param is set to empty object.</returns>
        public bool TryCreate(int dayOfMonth, int minTemp, int maxTemp, out IDailyWeather dailyWeather)
        {
            try
            {
                dailyWeather = new DailyWeather(dayOfMonth, minTemp, maxTemp);
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to create DailyWeather object from numeric values.", ex);
                dailyWeather = DailyWeather.EmptyDailyWeather;
            }

            return (DailyWeather)dailyWeather != DailyWeather.EmptyDailyWeather;
        }
    }
}
