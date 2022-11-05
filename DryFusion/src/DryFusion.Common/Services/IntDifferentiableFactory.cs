using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Common.Services
{
    /// <summary>
    /// Abstract class that implements a template pattern
    /// for creation of an object containing an integer difference.
    /// </summary>
    public abstract class IntDifferentiableFactory : IDifferentiableFactory<int>
    {
        protected readonly ILoggingService _loggingService;

        public IntDifferentiableFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Attempts to create a Differentiable object.
        /// </summary>
        public bool TryCreate(string id, string value1, string value2, IDifferentiable<int> fallback, out IDifferentiable<int> differentiable)
        {
            bool successfulParse;
            bool createdSuccessfully = false;

            try
            {
                successfulParse = int.TryParse(value1, out int parsedValue1);
                successfulParse &= int.TryParse(value2, out int parsedValue2);

                // Could throw an error to indicate there was an issue parsing with specific detail on what failed;
                // Because this application does not have specific error handling reqs, I like using the empty object 
                // here for simplicity.

                if (successfulParse)
                    createdSuccessfully = TryCreate(id, parsedValue1, parsedValue2, out differentiable);
                else
                    differentiable = fallback;
            }
            catch (Exception ex)
            {
                _loggingService.Log("Unable to create differentiable object from string values.", ex);
                differentiable = fallback;
            }
            return createdSuccessfully;
        }

        /// <summary>
        /// Template method implementation for subclasses.
        /// </summary>
        public abstract bool TryCreate(string id, int value1, int value2, out IDifferentiable<int> differentiable);
    }
}
