using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Common.Services
{
    /// <summary>
    /// Interface for classes that will create Differentiables.
    /// </summary>
    /// <typeparam name="T">Type that we will be using to calculate a difference.</typeparam>
    public interface IDifferentiableFactory<T>
    {
        /// <summary>
        /// Creates a differentiable object.
        /// </summary>
        /// <param name="id">Id for the new object.</param>
        /// <param name="value1">First of the values which we will use to calculate a difference.</param>
        /// <param name="value2">Second of the values which we will use to calculate a difference</param>
        /// <param name="differentiable">The created differentiable object.</param>
        /// <returns>True if creation was successful. False otherwise.</returns>
        bool TryCreate(string id, T value1, T value2, out IDifferentiable<T> differentiable);

        /// <summary>
        /// Creates a differentiable object.
        /// </summary>
        /// <param name="id">Id for the new object.</param>
        /// <param name="value1">First of the values which we will parse to T2 and use to calculate a difference.</param>
        /// <param name="value2">Second of the values which we will parse to T2 and use to calculate a difference</param>
        /// <param name="differentiable">The created differentiable object.</param>
        /// <returns>True if creation was successful. False otherwise.</returns>
        bool TryCreate(string id, string value1, string value2, IDifferentiable<T> fallback, out IDifferentiable<T> differentiable);
    }
}
