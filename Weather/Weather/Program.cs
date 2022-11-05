using Weather.Models;
using Weather.Services;
using System;
using System.Threading.Tasks;

namespace Weather
{
    public static class Program
    {
        private static ILoggingService _loggingService;
        private static IDailyWeatherProvider _dailyWeatherFileParser;

        public static async Task Main()
        {
            BuildDependencies();

            // For a real world application, we would want this file path configurable.
            // For now, this file is added to the project and copied to the output 
            // directory for simplicity._dailyWeatherFileParser

            var dailyWeatherService = new DailyWeatherService(_dailyWeatherFileParser, _loggingService);

            var smallestSpreadDay = await dailyWeatherService.GetWeatherWithSmallestTempSpread().ConfigureAwait(false);

            if((DailyWeather)smallestSpreadDay != DailyWeather.EmptyDailyWeather)
            {
                Console.Out.WriteLine($"Day number with minimum temperature spread: {smallestSpreadDay.DayOfMonth}");
            }
            else
            {
                Console.Out.WriteLine("Unable to determine day number with minimum temperature spread. See logs.");
            }
            Console.In.ReadLine();
        }

        private static void BuildDependencies()
        {
            // Wiring up dependencies manually for this simple application.
            // Could use DI framework if this were more complex.

            _loggingService = new LoggingService();
            var weatherFactory = new DailyWeatherFactory(_loggingService);
            _dailyWeatherFileParser = new DailyWeatherFileParser("weather.dat", weatherFactory, _loggingService);
        }
    }
}
