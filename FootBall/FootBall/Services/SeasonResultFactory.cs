using FootBall.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootBall.Services
{
 
    public class SeasonResultFactory : ISeasonResultFactory
    {
        
        
        public bool TryCreate(string team, string goalsFor, string goalsAgainst, out ISeasonResult seasonResult)
        {
            bool successfulParse;
            try
            {
                successfulParse = int.TryParse(goalsFor, out int parsedGoalsFor);
                successfulParse &= int.TryParse(goalsAgainst, out int parsedGoalsAgainst);              

                if (successfulParse)
                    TryCreate(team, parsedGoalsFor, parsedGoalsAgainst, out seasonResult);
                else
                    seasonResult = SeasonResult.EmptySeasonResult;
            }
            catch(Exception ex)
            {
               
                seasonResult = SeasonResult.EmptySeasonResult;
            }
            return (SeasonResult)seasonResult != SeasonResult.EmptySeasonResult;
        }

    
        public bool TryCreate(string team, int goalsFor, int goalsAgainst, out ISeasonResult seasonResult)
        {
            try
            {
                seasonResult = new SeasonResult(team, goalsFor, goalsAgainst);
            }
            catch(Exception ex)
            {
               
                seasonResult = SeasonResult.EmptySeasonResult;
            }

            return (SeasonResult)seasonResult != SeasonResult.EmptySeasonResult;
        }
    }
}
