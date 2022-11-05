using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Football.Models
{
    public interface ISeasonResult : IDifferentiable<int>
    {
        string Team { get; }
    }
}
