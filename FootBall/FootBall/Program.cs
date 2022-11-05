using FootBall.Models;
using FootBall.Services;
using System;
using System.Threading.Tasks;

namespace FootBall
{
    public static class Program
    {
        private static ILoggingService _loggingService;
        private static ISeasonResultProvider _seasonResultFileParser;

        public static async Task Main()
        {
            BuildDependencies();

            // For a real world application, we would want this file path configurable.
            // For now, this file is added to the project and copied to the output 
            // directory for simplicity.

            var seasonResultService = new SeasonResultService(_seasonResultFileParser, _loggingService);

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

        private static void BuildDependencies()
        {
            // Wiring up dependencies manually for this simple application.
            // Could use DI framework if this were more complex.

            _loggingService = new LoggingService();
            var seasonResultFactory = new SeasonResultFactory(_loggingService);
            _seasonResultFileParser = new SeasonResultFileParser("football.dat", seasonResultFactory, _loggingService);
        }
    }
}
