using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Weather.Models
{
    public interface IDailyWeather : IDifferentiable<int>
    {
        int DayOfMonth { get; }
    }
}
