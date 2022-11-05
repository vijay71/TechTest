using FootBall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBall.Services
{
    /// <summary>
    /// Used to encapsulate logic associated with the collection of season result records.
    /// </summary>
    public class SeasonResultService : ISeasonResultService
    {
        private readonly ISeasonResultProvider _seasonResultProvider;
        private readonly ILoggingService _loggingService;
        private IEnumerable<ISeasonResult> _seasonResults;

        public SeasonResultService(ISeasonResultProvider seasonResultProvider, ILoggingService loggingService)
        {
            _seasonResultProvider = seasonResultProvider;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Finds the SeasonResult object with the smallest differential between goals scored for and against.
        /// </summary>
        /// <returns>The SeasonResult object with the smallest differential between goals for and against.</returns>
        public async Task<ISeasonResult> GetSeasonResultWithSmallestPointDifferential()
        {
            // If this application were any larger/complex, we would need to abstract out this initialization
            // of the _seasonResults collection. If we were to add more methods to this class, we would
            // have to make this check in each new method that used the collection which would not be good.

            if (_seasonResults == null) await GetSeasonResults().ConfigureAwait(false);

            // Determine the smallest goal differential in the list.
            // Assumes that it's acceptable to return the first one in the
            // list if two or more days have the same minimum spread.

            return _seasonResults.Any() ? _seasonResults.Aggregate((w1, w2) =>
                Math.Abs(w1.GoalsFor - w1.GoalsAgainst) < Math.Abs(w2.GoalsFor - w2.GoalsAgainst) ? w1 : w2) : SeasonResult.EmptySeasonResult;
        }

        private async Task GetSeasonResults()
        {
            try
            {
                _seasonResults = await _seasonResultProvider.GetSeasonResults().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to retrieve season result data.", ex);
                _seasonResults = new ISeasonResult[0];
            }
        }
    }
}
