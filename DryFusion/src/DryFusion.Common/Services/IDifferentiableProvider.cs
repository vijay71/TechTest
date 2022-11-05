using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DryFusion.Common.Services
{
    public interface IDifferentiableProvider<T>
    {
        Task<IEnumerable<IDifferentiable<T>>> GetDifferentiables();
    }
}
