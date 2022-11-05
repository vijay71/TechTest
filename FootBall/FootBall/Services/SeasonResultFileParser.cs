using FootBall.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootBall.Services
{
    
    public class SeasonResultFileParser : ISeasonResultProvider
    {
        private readonly string _filePath;
        private readonly ISeasonResultFactory _seasonResultFactory;
       

        public SeasonResultFileParser(string filePath, ISeasonResultFactory seasonResultFactory)
        {
            _filePath = filePath;
            _seasonResultFactory = seasonResultFactory;
          
        }

        public async Task<IEnumerable<ISeasonResult>> GetSeasonResults()
        {
            var seasonResults = new List<ISeasonResult>();

          
                using (var sr = new StreamReader(_filePath))
                {
                    string line;
                    ISeasonResult parsedSeasonResult;

                   

                    await sr.ReadLineAsync().ConfigureAwait(false);

                    while ((line = await sr.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                       
                        line = line.Trim();
                        Regex rx = new Regex(@"\s+");
                        line = rx.Replace(line, ",");

                        var resultValues = line.Split(',');

                     

                        if (resultValues.Length > 8 && _seasonResultFactory.TryCreate(resultValues[1],
                            resultValues[6], resultValues[8], out parsedSeasonResult))
                        {
                            seasonResults.Add(parsedSeasonResult);
                        }
                    }
                }
           

            return seasonResults;
        }
    }
}
