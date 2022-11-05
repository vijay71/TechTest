using DryFusion.Common.Services;
using DryFusion.Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryFusion.Weather.Services
{
    /// <summary>
    /// Used to encapsulate logic associated with the collection of daily weather records.
    /// </summary>
    public class DailyWeatherService : IntDifferentiableService
    {
        public DailyWeatherService(IDifferentiableProvider<int> dailyWeatherProvider, ILoggingService loggingService)
            :base(dailyWeatherProvider, loggingService)
        {
        }
    }
}
