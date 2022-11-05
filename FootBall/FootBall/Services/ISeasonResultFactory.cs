using FootBall.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootBall.Services
{
    public interface ISeasonResultFactory
    {
        bool TryCreate(string team, string goalsFor, string goalsAgainst, out ISeasonResult seasonResult);
        bool TryCreate(string team, int goalsFor, int goalsAgainst, out ISeasonResult seasonResult);
    }
}
