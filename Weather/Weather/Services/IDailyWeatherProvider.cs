using Weather.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Services
{
    public interface IDailyWeatherProvider
    {
        Task<IEnumerable<IDailyWeather>> GetDailyWeathers();
    }
}
