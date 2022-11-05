using DryFusion.Common.Services;
using DryFusion.Football.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Football.Services
{
    public class SeasonResultService : IntDifferentiableService
    {
        public SeasonResultService(IDifferentiableProvider<int> resultProvider, ILoggingService loggingService) 
            : base(resultProvider, loggingService)
        {
        }
    }
}
