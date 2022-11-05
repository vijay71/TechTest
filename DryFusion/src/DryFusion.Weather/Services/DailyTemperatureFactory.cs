using DryFusion.Common.Models;
using DryFusion.Common.Services;
using DryFusion.Weather.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Weather.Services
{
    /// <summary>
    /// Encapsulates the logic needed to create a Daily Temperature object.
    /// </summary>
    public class DailyTemperatureFactory : IntDifferentiableFactory
    {
        public DailyTemperatureFactory(ILoggingService loggingService) : base(loggingService)
        {
        }

        /// <summary>
        /// Creates a DailyTemperature object.
        /// </summary>
        /// <returns>True if object successfully created with input params. False if not and out param is set to empty object.</returns>
        public override bool TryCreate(string dayOfMonth, int minTemp, int maxTemp, out IDifferentiable<int> dailyWeather)
        {
            try
            {
                var successful = int.TryParse(dayOfMonth, out int numDayOfMonth);
                dailyWeather = successful ? new DailyTemperature(numDayOfMonth, minTemp, maxTemp) : DailyTemperature.EmptyDailyWeather;
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to create DailyWeather object from numeric values.", ex);
                dailyWeather = DailyTemperature.EmptyDailyWeather;
            }

            return (DailyTemperature)dailyWeather != DailyTemperature.EmptyDailyWeather;
        }
    }
}
