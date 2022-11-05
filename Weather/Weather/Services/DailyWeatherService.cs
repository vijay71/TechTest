using Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Services
{
    /// <summary>
    /// Used to encapsulate logic associated with the collection of daily weather records.
    /// </summary>
    public class DailyWeatherService : IDailyWeatherService
    {
        private readonly IDailyWeatherProvider _fileParser;
        private readonly ILoggingService _loggingService;
        private IEnumerable<IDailyWeather> _dailyWeathers;

        public DailyWeatherService(IDailyWeatherProvider fileParser, ILoggingService loggingService)
        {
            _fileParser = fileParser;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Finds the DailyWeather object with the smallest differential between min and max temp.
        /// </summary>
        /// <returns>The DailyWeather object with the smallest differential between min and max temp.</returns>
        public async Task<IDailyWeather> GetWeatherWithSmallestTempSpread()
        {
            // If this application were any larger/complex, we would need to abstract out this initialization
            // of the _dailyWeathers collection. If we were to add more methods to this class, we would
            // have to make this check in each new method that used the collection which would not be good.

            if (_dailyWeathers == null) await GetDailyWeathers().ConfigureAwait(false);

            // Determine the smallest temp spread in the list.
            // Assumes that it's acceptable to return the first one in the
            // list if two or more days have the same minimum spread.

            return _dailyWeathers.Any() ? _dailyWeathers.Aggregate((w1, w2) =>
                Math.Abs(w1.MaxTemp - w1.MinTemp) < Math.Abs(w2.MaxTemp - w2.MinTemp) ? w1 : w2) : DailyWeather.EmptyDailyWeather;
        }

        private async Task GetDailyWeathers()
        {
            try
            {
                _dailyWeathers = await _fileParser.GetDailyWeathers().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to retrieve daily weather data.", ex);
                _dailyWeathers = new IDailyWeather[0];
            }
        }
    }
}
