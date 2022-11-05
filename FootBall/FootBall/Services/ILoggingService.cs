using System;
using System.Collections.Generic;
using System.Text;

namespace FootBall.Services
{
    public interface ILoggingService
    {
        void Log(string message, Exception ex = null);
    }
}
