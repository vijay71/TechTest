using FootBall.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootBall.Services
{
    public interface ISeasonResultService
    {
        Task<ISeasonResult> GetSeasonResultWithSmallestPointDifferential();
    }
}
