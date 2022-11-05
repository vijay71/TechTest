using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Football.Models
{
    /// <summary>
    /// Represents a record of a team's EPL season goals result
    /// </summary>
    public class SeasonGoalsResult : IntDifferentiable, ISeasonGoalsResult
    {
        public static SeasonGoalsResult EmptySeasonResult = new SeasonGoalsResult();
        
        public string Team { get; }
        public int GoalsFor { get; }
        public int GoalsAgainst { get; }

        public SeasonGoalsResult(string team, int goalsFor, int goalsAgainst) : base(goalsFor, goalsAgainst)
        {
            if (goalsFor < 0 || goalsAgainst < 0) throw new ArgumentException("Goals for and against must be 0 or greater.");
            Team = team;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
        }

        // Private constructor used for empty object

        private SeasonGoalsResult() : base(0,0) { Team = String.Empty; }

        // Equality operations overridden for completeness. The intent is that
        // this application treats IntDifferentiable as a value object.

        public override bool Equals(Object obj)
        {
            return obj is SeasonGoalsResult seasonResult && this == seasonResult;
        }

        public override int GetHashCode()
        {
            return Team.GetHashCode() ^ AbsoluteDifference.GetHashCode();
        }

        public static bool operator ==(SeasonGoalsResult x, SeasonGoalsResult y)
        {
            return x.Team == y.Team && x.AbsoluteDifference == y.AbsoluteDifference;
        }

        public static bool operator !=(SeasonGoalsResult x, SeasonGoalsResult y)
        {
            return !(x == y);
        }
    }
}
