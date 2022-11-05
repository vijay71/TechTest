using DryFusion.Common.Models;
using DryFusion.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DryFusion.Common
{
    /// <summary>
    /// Contains methods that can be used to display differentiable data for multiple console applications.
    /// </summary>
    /// <typeparam name="T">Type used to calculate the differentiable value.</typeparam>
    public class ConsoleDifferentiableDisplayer<T> : IConsoleDifferentiableDisplayer<T>
    {
        private readonly IDifferentiableService<T> _differentiableService;

        public ConsoleDifferentiableDisplayer(IDifferentiableService<T> differentiableService)
        {
            _differentiableService = differentiableService;
        }

        /// <summary>
        /// Finds and displays the minimum item in a collection of differentiables
        /// </summary>
        /// <typeparam name="T2">Type of differentiable we want to find the minimum for.</typeparam>
        /// <param name="successMessage">Will display when program succesfully finds the minimum.</param>
        /// <param name="failureMessage">Will display when program unsuccesfully finds the minimum.</param>
        /// <returns></returns>
        public async Task DisplayMinDifferential<T2>(Func<T2, string> successMessage, string failureMessage, T2 defaultResult)
            where T2 : class, IDifferentiable<T>
        {
            // For a real world application, we would want this file path configurable.
            // For now, this file is added to the project and copied to the output 
            // directory for simplicity.

            var minDifferential = await _differentiableService.GetAbsoluteMinimumDifferentiable(defaultResult).ConfigureAwait(false);

            if (minDifferential != defaultResult)
            {
                Console.Out.WriteLine(successMessage(minDifferential));
            }
            else
            {
                Console.Out.WriteLine(failureMessage);
            }
            Console.In.ReadLine();
        }
    }
}
