using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DryFusion.Common.Services
{
    /// <summary>
    /// Interface for a class that will implement functionality on a collection
    /// of IDifferentiables.
    /// </summary>
    /// <typeparam name="T">Type of the IDifferentiable will use to determine a difference.</typeparam>
    public interface IDifferentiableService<T>
    {
        /// <summary>
        /// Returns the data item with the smallest difference between its fields
        /// </summary>
        /// <typeparam name="T2">Type for which we will filter for before performing operation</typeparam>
        /// <param name="fallback"></param>
        /// <returns></returns>
        Task<T2> GetAbsoluteMinimumDifferentiable<T2>(T2 fallback)
            where T2 : IDifferentiable<T>;
    }
}
