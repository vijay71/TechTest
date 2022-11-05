using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Services
{
    public interface ILoggingService
    {
        void Log(string message, Exception ex = null);
    }
}
