using System;
using System.Collections.Generic;
using System.Text;

namespace FootBall.Models
{
    public interface ISeasonResult
    {
        string Team { get; }
        int GoalsFor { get; }
        int GoalsAgainst { get; }
    }
}
