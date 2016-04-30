using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace B3MobileApp.Helpers
{
    public interface ILogger
    {
        void Log(string message, string source = "Undefined", LogType logType = LogType.Debug);
    }

    public enum LogType
    {
        Debug, Error, Info
    }
}
