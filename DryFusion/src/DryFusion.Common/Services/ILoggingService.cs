using System;
using System.Collections.Generic;
using System.Text;

namespace DryFusion.Common.Services
{
    public interface ILoggingService
    {
        void Log(string message, Exception ex = null);
    }
}
