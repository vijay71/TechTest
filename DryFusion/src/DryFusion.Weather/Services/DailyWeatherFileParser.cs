using DryFusion.Common.Models;
using DryFusion.Common.Services;
using DryFusion.Weather.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DryFusion.Weather.Services
{
    /// <summary>
    /// Encapsulates the logic needed to parse a weather data file for Kata04.
    /// </summary>
    public class DailyWeatherFileParser : IDifferentiableProvider<int>
    {
        private readonly IDifferentiableFactory<int> _dailyWeatherFactory;
        private readonly ILoggingService _loggingService;
        private readonly string _filePath;

        public DailyWeatherFileParser(string filePath, IDifferentiableFactory<int> dailyWeatherFactory, ILoggingService loggingService)
        {
            _filePath = filePath;
            _dailyWeatherFactory = dailyWeatherFactory;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates DailyWeather objects based on each record in the data file.
        /// </summary>
        /// <param name="filePath">Path of the data file containing daily weather records.</param>
        /// <returns>A collection of DailyWeather objects that were present in the input file.</returns>
        public async Task<IEnumerable<IDifferentiable<int>>> GetDifferentiables()
        {
            var dailyWeathers = new List<IDifferentiable<int>>();

            try
            {
                using (var sr = new StreamReader(_filePath))
                {
                    string line;
                    IDifferentiable<int> parsedDailyWeather;

                    // Ignore header line and empty first row.
                    // This implementation assumes the application code will have
                    // to be modified and recompiled if the file format changes.

                    await sr.ReadLineAsync().ConfigureAwait(false);
                    await sr.ReadLineAsync().ConfigureAwait(false);

                    while ((line = await sr.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        // Replace spaces with commas to simplify parsing and avoid parsing
                        // by hard-coded character index values. Using regex to handle multiple spaces.

                        line = line.Trim();
                        Regex rx = new Regex(@"\s+");
                        line = rx.Replace(line, ",");

                        // Strip out the asterisks bc we don't need them
                        // for this exercise.

                        line = line.Replace("*", String.Empty);

                        var weatherValues = line.Split(',');

                        // The factory.TryCreate method will handle the last row of the file
                        // that contains statistics for the month that we don't use.

                        if (weatherValues.Length > 2 && _dailyWeatherFactory.TryCreate(weatherValues[0],
                            weatherValues[2], weatherValues[1], DailyTemperature.EmptyDailyWeather, out parsedDailyWeather))
                        {
                            dailyWeathers.Add(parsedDailyWeather);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to parse weather data file.", ex);
            }

            return dailyWeathers;
        }
    }
}
