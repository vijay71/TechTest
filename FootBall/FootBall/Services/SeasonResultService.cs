using FootBall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootBall.Services
{
    
    public class SeasonResultService : ISeasonResultService
    {
        private readonly ISeasonResultProvider _seasonResultProvider;
       
        private IEnumerable<ISeasonResult> _seasonResults;

        public SeasonResultService(ISeasonResultProvider seasonResultProvider)
        {
            _seasonResultProvider = seasonResultProvider;
           
        }

        
        public async Task<ISeasonResult> GetSeasonResultWithSmallestPointDifferential()
        {
          

            if (_seasonResults == null) await GetSeasonResults().ConfigureAwait(false);

         

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
               
                _seasonResults = new ISeasonResult[0];
            }
        }
    }
}
