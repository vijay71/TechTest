using Weather.Models;
using Weather.Services;
using System;
using System.Threading.Tasks;

namespace Weather
{
    public static class Program
    {
        
        private static IDailyWeatherProvider _dailyWeatherFileParser;

        public static async Task Main()
        {
          

          

            var dailyWeatherService = new DailyWeatherService(_dailyWeatherFileParser);

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

       
    }
}
