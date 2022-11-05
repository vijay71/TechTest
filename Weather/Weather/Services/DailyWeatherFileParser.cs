using Weather.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Weather.Services
{
    
    public class DailyWeatherFileParser : IDailyWeatherProvider
    {
        private readonly string _filePath;
        private readonly IDailyWeatherFactory _dailyWeatherFactory;
       

        public DailyWeatherFileParser(string filePath, IDailyWeatherFactory dailyWeatherFactory)
        {
            _filePath = filePath;
            _dailyWeatherFactory = dailyWeatherFactory;
           
        }

       
        public async Task<IEnumerable<IDailyWeather>> GetDailyWeathers()
        {
            var dailyWeathers = new List<IDailyWeather>();

          
                using (var sr = new StreamReader(_filePath))
                {
                    string line;
                    IDailyWeather parsedDailyWeather;

                  

                    await sr.ReadLineAsync().ConfigureAwait(false);
                    await sr.ReadLineAsync().ConfigureAwait(false);

                    while ((line = await sr.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        

                        line = line.Trim();
                        Regex rx = new Regex(@"\s+");
                        line = rx.Replace(line, ",");

                    

                        line = line.Replace("*", String.Empty);

                        var weatherValues = line.Split(',');

                       

                        if (weatherValues.Length > 2 && _dailyWeatherFactory.TryCreate(weatherValues[0],
                            weatherValues[2], weatherValues[1], out parsedDailyWeather))
                        {
                            dailyWeathers.Add(parsedDailyWeather);
                        }
                    }
                }
           

            return dailyWeathers;
        }
    }
}
