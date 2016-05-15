using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mtapp.Droid.Helpers;
using Mtapp.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(FileLogger))]
namespace Mtapp.Droid.Helpers
{
    public class FileLogger : ILogger
    {
        private static readonly object LockObject = new object();

        private readonly string _logsFilePath;

        public FileLogger()
        {
            var appDataDirPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var logsDirPath = Path.Combine(appDataDirPath, "logs");

            if (!Directory.Exists(logsDirPath))
                Directory.CreateDirectory(logsDirPath);

            _logsFilePath = Path.Combine(logsDirPath, DateTime.Now.ToString("s"));

            var initialLogMessage = "MTAPP log file created";
            Log(initialLogMessage, "FileLogger");
        }

        public void Log(string message, string source = "Undefined", LogType logType = LogType.Debug)
        {
            var type = string.Empty;
            switch (logType)
            {
                case LogType.Debug:
                    type = "DEBUG";
                    Android.Util.Log.Debug(source, message);
                    break;

                case LogType.Error:
                    type = "ERROR";
                    Android.Util.Log.Error(source, message);
                    break;

                case LogType.Info:
                    type = "INFO ";
                    Android.Util.Log.Info(source, message);
                    break;
            }

            var formattedMessage = CreateLogMessage(message, source, type);
            AppendLogToFile(formattedMessage);
        }

        private string CreateLogMessage(string log, string source, string type)
        {
            return string.Format("{0} {1} {2}: {3}{4}", DateTime.Now.ToString("s"), type, source, log,
                Environment.NewLine);
        }

        private void AppendLogToFile(string logMessage)
        {
            lock (LockObject)
            {
                try
                {
                    File.AppendAllText(_logsFilePath, logMessage);
                }
                catch (IOException ex)
                {
                    Android.Util.Log.Error("FileLogger", ex.Message);
                }
            }
        }
    }
}