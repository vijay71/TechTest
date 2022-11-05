using FootBall.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootBall.Services
{
    /// <summary>
    /// Encapsulates the logic needed to create a SeasonResult object.
    /// </summary>
    public class SeasonResultFactory : ISeasonResultFactory
    {
        private readonly ILoggingService _loggingService;

        public SeasonResultFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Attempts to create a SeasonResult object.
        /// </summary>
        /// <param name="team">String representing the team name.</param>
        /// <param name="goalsFor">String representing the goals a team scored. Should be a numeric value.</param>
        /// <param name="goalsAgainst">String representing the goals scored on a team. Should be a numeric value.</param>
        /// <param name="seasonResult">A SeasonResult object created from the input values (or an empty implementation if parsing failed).</param>
        /// <returns>True if object successfully created with input params. False if not and out param is set to empty object.</returns>
        public bool TryCreate(string team, string goalsFor, string goalsAgainst, out ISeasonResult seasonResult)
        {
            bool successfulParse;
            try
            {
                successfulParse = int.TryParse(goalsFor, out int parsedGoalsFor);
                successfulParse &= int.TryParse(goalsAgainst, out int parsedGoalsAgainst);

                // Could throw an error to indicate there was an issue parsing with specific detail on what failed;
                // Because this application does not have specific error handling reqs, I like using the empty object 
                // here for simplicity.

                if (successfulParse)
                    TryCreate(team, parsedGoalsFor, parsedGoalsAgainst, out seasonResult);
                else
                    seasonResult = SeasonResult.EmptySeasonResult;
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to create SeasonResult object from string values.", ex);
                seasonResult = SeasonResult.EmptySeasonResult;
            }
            return (SeasonResult)seasonResult != SeasonResult.EmptySeasonResult;
        }

        /// <summary>
        /// Creates a SeasonResult object.
        /// </summary>
        /// <returns>True if object successfully created with input params. False if not and out param is set to empty object.</returns>
        public bool TryCreate(string team, int goalsFor, int goalsAgainst, out ISeasonResult seasonResult)
        {
            try
            {
                seasonResult = new SeasonResult(team, goalsFor, goalsAgainst);
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to create SeasonResult object from numeric values.", ex);
                seasonResult = SeasonResult.EmptySeasonResult;
            }

            return (SeasonResult)seasonResult != SeasonResult.EmptySeasonResult;
        }
    }
}
