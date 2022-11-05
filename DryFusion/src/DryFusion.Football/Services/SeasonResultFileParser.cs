using DryFusion.Common.Models;
using DryFusion.Common.Services;
using DryFusion.Football.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DryFusion.Football.Services
{
    /// <summary>
    /// Encapsulates the logic needed to parse a season result data file for Kata04.
    /// </summary>
    public class SeasonResultFileParser : IDifferentiableProvider<int>
    {
        private readonly IDifferentiableFactory<int> _seasonResultFactory;
        private readonly ILoggingService _loggingService;
        private readonly string _filePath;

        public SeasonResultFileParser(string filePath, IDifferentiableFactory<int> seasonResultFactory, ILoggingService loggingService)
        {
            _filePath = filePath;
            _seasonResultFactory = seasonResultFactory;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates SeasonResult objects based on each record in the data file.
        /// </summary>
        /// <param name="filePath">Path of the data file containing season result records.</param>
        /// <returns>A collection of SeasonResult objects that were present in the input file.</returns>
        public async Task<IEnumerable<IDifferentiable<int>>> GetDifferentiables()
        {
            var seasonResults = new List<IDifferentiable<int>>();

            try
            {
                using (var sr = new StreamReader(_filePath))
                {
                    string line;
                    IDifferentiable<int> parsedSeasonResult = null;

                    // Ignore header row.
                    // This implementation assumes the application code will have
                    // to be modified and recompiled if the file format changes.

                    await sr.ReadLineAsync().ConfigureAwait(false);

                    while ((line = await sr.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        // Replace spaces with commas to simplify parsing and avoid parsing
                        // by hard-coded character index values. Using regex to handle multiple spaces.

                        line = line.Trim();
                        Regex rx = new Regex(@"\s+");
                        line = rx.Replace(line, ",");

                        var resultValues = line.Split(',');

                        // The factory.TryCreate method will handle rows which don't conform to the schema
                        // we need.

                        if (resultValues.Length > 8 && _seasonResultFactory.TryCreate(resultValues[1],
                            resultValues[6], resultValues[8], SeasonGoalsResult.EmptySeasonResult, out parsedSeasonResult))
                        {
                            seasonResults.Add(parsedSeasonResult);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _loggingService.Log("Unable to parse season result data file.", ex);
            }

            return seasonResults;
        }
    }
}
