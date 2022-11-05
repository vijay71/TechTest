using DryFusion.Common.Models;
using DryFusion.Common.Services;
using DryFusion.Football.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Football.Services
{
    /// <summary>
    /// Encapsulates the logic needed to create a SeasonGoalsResult object.
    /// </summary>
    public class SeasonGoalsResultFactory : IntDifferentiableFactory
    {
        public SeasonGoalsResultFactory(ILoggingService loggingService) : base(loggingService)
        {
        }

        /// <summary>
        /// Creates a SeasonGoalsResult object.
        /// </summary>
        /// <returns>True if object successfully created with input params. False if not and out param is set to empty object.</returns>
        public override bool TryCreate(string team, int goalsFor, int goalsAgainst, out IDifferentiable<int> seasonGoalsResult)
        {
            try
            {
                seasonGoalsResult = new SeasonGoalsResult(team, goalsFor, goalsAgainst);
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to create SeasonResult object from numeric values.", ex);
                seasonGoalsResult = SeasonGoalsResult.EmptySeasonResult;
            }

            return (SeasonGoalsResult)seasonGoalsResult != SeasonGoalsResult.EmptySeasonResult;
        }
    }
}
