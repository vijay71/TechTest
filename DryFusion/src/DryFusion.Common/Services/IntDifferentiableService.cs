using DryFusion.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryFusion.Common.Services
{
    /// <summary>
    /// Implements logic for the IDifferentiableService interface.
    /// </summary>
    public abstract class IntDifferentiableService : IDifferentiableService<int>
    {
        protected readonly ILoggingService _loggingService;
        protected readonly IDifferentiableProvider<int> _dataProvider;
        protected IEnumerable<IDifferentiable<int>> _differentiables;

        public IntDifferentiableService(IDifferentiableProvider<int> dataProvider, ILoggingService loggingService)
        {
            _dataProvider = dataProvider;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Returns the minimum differential from the collection
        /// </summary>
        /// <typeparam name="T">Type of differential we are searching for.</typeparam>
        /// <param name="fallback">Defualt value if unable to find a result.</param>
        /// <returns>The minimum differential from the collection.</returns>
        public async Task<T> GetAbsoluteMinimumDifferentiable<T>(T fallback)
            where T : IDifferentiable<int>
        {
            // If this application were any larger/complex, we would need to abstract out this initialization
            // of the _differentiables collection. If we were to add more methods to this class, we would
            // have to make this check in each new method that used the collection which would not be good.

            if (_differentiables == null) _differentiables = (await LoadDifferentiables().ConfigureAwait(false));

            var filteredDifferentiables = _differentiables.Where(d => d is T).Select(d => (T)d);

            // Determine the smallest differential in the list.
            // Assumes that it's acceptable to return the first one in the
            // list if two or more days have the same difference.

            return filteredDifferentiables.Any() ? filteredDifferentiables.Aggregate((d1, d2) =>
                d1.AbsoluteDifference < d2.AbsoluteDifference ? d1 : d2) : fallback;
        }

        private async Task<IEnumerable<IDifferentiable<int>>> LoadDifferentiables()
        {
            try
            {
                return await _dataProvider.GetDifferentiables().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _loggingService.Log("Unable to retrieve season result data.", ex);
                return Enumerable.Empty<IDifferentiable<int>>();
            }
        }
    }
}
