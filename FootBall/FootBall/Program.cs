using FootBall.Models;
using FootBall.Services;
using System;
using System.Threading.Tasks;

namespace FootBall
{
    public static class Program
    {
       
        private static ISeasonResultProvider _seasonResultFileParser;

        public static async Task Main()
        {
           

           

            var seasonResultService = new SeasonResultService(_seasonResultFileParser);

            var smallestDifferntialResult = await seasonResultService.GetSeasonResultWithSmallestPointDifferential().ConfigureAwait(false);

            if((SeasonResult)smallestDifferntialResult != SeasonResult.EmptySeasonResult)
            {
                Console.Out.WriteLine($"Team with smallest point differential: {smallestDifferntialResult.Team}");
            }
            else
            {
                Console.Out.WriteLine("Unable to determine team with smallest point differential. See logs.");
            }
            Console.In.ReadLine();
        }
       
    }
}
