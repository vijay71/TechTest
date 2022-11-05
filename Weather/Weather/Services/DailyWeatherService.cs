using Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Services
{
  
    public class DailyWeatherService : IDailyWeatherService
    {
        private readonly IDailyWeatherProvider _fileParser;
        private IEnumerable<IDailyWeather> _dailyWeathers;

        public DailyWeatherService(IDailyWeatherProvider fileParser)
        {
            _fileParser = fileParser;
           
        }

     
        public async Task<IDailyWeather> GetWeatherWithSmallestTempSpread()
        {
            

            if (_dailyWeathers == null) await GetDailyWeathers().ConfigureAwait(false);

           

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
              
                _dailyWeathers = new IDailyWeather[0];
            }
        }
    }
}
