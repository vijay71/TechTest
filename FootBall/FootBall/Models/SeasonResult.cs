using System;
using System.Collections.Generic;
using System.Text;

namespace FootBall.Models
{
    /// <summary>
    /// Represents a record of a team's EPL season result
    /// </summary>
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

        // Private constructor used for empty object

        private SeasonResult() { Team = string.Empty; }

        // Equality operations overridden for completeness. The intent is that
        // this application treats SeasonResult as a value object.

        public override bool Equals(Object obj)
        {
            return obj is SeasonResult seasonResult && this == seasonResult;
        }

        public override int GetHashCode()
        {
            return Team.GetHashCode() ^ GoalsFor.GetHashCode() ^ GoalsAgainst.GetHashCode();
        }

        public static bool operator ==(SeasonResult x, SeasonResult y)
        {
            return x.Team == y.Team && x.GoalsFor == y.GoalsFor && x.GoalsAgainst == y.GoalsAgainst;
        }

        public static bool operator !=(SeasonResult x, SeasonResult y)
        {
            return !(x == y);
        }
    }
}
