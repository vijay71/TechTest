using DryFusion.Common;
using DryFusion.Common.Services;
using DryFusion.Football.Models;
using DryFusion.Football.Services;
using System;
using System.Threading.Tasks;

namespace DryFusion.Football
{
    public static class Program
    {
        private static ILoggingService _loggingService;
        private static IDifferentiableProvider<int> _seasonGoalsResultFileParser;

        public static async Task Main()
        {
            BuildDependencies();

            // For a real world application, we would want this file path configurable.
            // For now, this file is added to the project and copied to the output 
            // directory for simplicity.

            var seasonResultService = new SeasonResultService(_seasonGoalsResultFileParser, _loggingService);

            var programRunner = new ConsoleDifferentiableDisplayer<int>(seasonResultService);

            await programRunner.DisplayMinDifferential<ISeasonGoalsResult>(
                (result) => $"Team with smallest point differential: { result.Team }",
                "Unable to determine team with smallest point differential. See logs.",
                SeasonGoalsResult.EmptySeasonResult).ConfigureAwait(false);
        }

        private static void BuildDependencies()
        {
            // Wiring up dependencies manually for this simple application.
            // Could use DI framework if this were more complex.

            _loggingService = new LoggingService();
            var seasonGoalsResultFactory = new SeasonGoalsResultFactory(_loggingService);
            _seasonGoalsResultFileParser = new SeasonResultFileParser("football.dat", seasonGoalsResultFactory, _loggingService);
        }
    }
}
