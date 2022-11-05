using System;
using System.Collections.Generic;
using System.Text;

namespace FootBall.Models
{
    
    public class SeasonResult : ISeasonResult
    {
        public static SeasonResult EmptySeasonResult = new SeasonResult();

        public string Team { get; }
        public int GoalsFor { get; }
        public int GoalsAgainst { get; }

        public SeasonResult(string team, int goalsFor, int goalsAgainst)
        {
            if (goalsFor < 0 || goalsAgainst < 0) throw new ArgumentException("Goals for and against must be 0 or greater.");

            Team = team;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
        }    

      private SeasonResult() { Team = string.Empty; }

        
    }
}
