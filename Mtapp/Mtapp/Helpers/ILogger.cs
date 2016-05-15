using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtapp.Helpers
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
