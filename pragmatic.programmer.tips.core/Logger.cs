using Newtonsoft.Json;

namespace pragmatic.programmer.tips.core
{
    /// <summary>
    /// generic logging class used to log to the console or a file.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// options for where to log to.
        /// </summary>
        public enum LogTo
        {
            Console,
            File
        }

        /// <summary>
        /// options for the log level.
        /// </summary>
        public enum LogLevel
        {
            Debug,
            Information,
            Warning,
            Error,
        }

        private readonly LogTo _logTo;
        private readonly string _logFileLocation;
        private LogLevel _logLevel;

        public Logger(LogTo logTo = LogTo.Console, LogLevel logLevel = LogLevel.Information)
        {
            _logTo = logTo;
            _logLevel = logLevel;

            _logFileLocation = string.Empty;
        }

        public Logger(LogTo logTo = LogTo.File, string logFileLocation = "", LogLevel logLevel = LogLevel.Information)
        {
            _logTo = logTo;
            _logFileLocation = logFileLocation;
            _logLevel = logLevel;
        }

        // TODO: Add log file clearer on override that removes unwanted logs from file.
        /// <summary>
        /// overrides the highest logLevel to log
        /// </summary>
        /// <param name="logLevel">the new LogLevel to set</param>
        public void OverrideLogLevel(LogLevel logLevel)
            => _logLevel = logLevel;

        /// <summary>
        /// send a generic log as debug.
        /// </summary>
        /// <param name="log">the log message</param>
        public async Task LogDebug(string log)
            => await Log(log, LogLevel.Debug);

        /// <summary>
        /// send a generic log as information.
        /// </summary>
        /// <param name="log">the log message</param>
        public async Task Log(string log)
            => await Log(log, LogLevel.Information);

        /// <summary>
        /// send a generic log as a warning.
        /// </summary>
        /// <param name="log">the log message</param>
        public async Task LogWarning(string log)
            => await Log(log, LogLevel.Warning);

        /// <summary>
        /// send a generic log as an error.
        /// </summary>
        /// <param name="log">the log message</param>
        /// <param name="exception">an optional exception to be included in the log</param>
        public async Task LogError(string log, Exception? exception = null)
        {
            // no additional exception details, just log message
            if (exception == null)
            {
                await Log(log, LogLevel.Error);
                return;
            }

            // add exception details to log message
            var exceptionAsJson = JsonConvert.SerializeObject(exception);
            var bloatedLog = $"{log}: {exceptionAsJson}";
            await Log(bloatedLog, LogLevel.Error);
        }

        /// <summary>
        /// private log method where timestamp is added to the log and the displaying/outputting of the log is done.
        /// </summary>
        /// <param name="log">the log message</param>
        /// <param name="logLevel">the log level of the log message</param>
        /// <returns></returns>
        private async Task Log(string log, LogLevel logLevel)
        {
            // skip log if loglevel is less than global loglevel
            if (logLevel < _logLevel)
                return;

            var timestamp = DateTime.Now;
            var logLevelAsString = Enum.GetName(typeof(LogLevel), logLevel)?.ToUpper();
            var bloatedLog = $"[@{timestamp:u} | {logLevelAsString}]: {log}";

            switch (_logTo)
            {
                case LogTo.Console:
                    Console.WriteLine(bloatedLog);
                    break;

                case LogTo.File:

                    if (!Directory.Exists(_logFileLocation))
                        Directory.CreateDirectory(_logFileLocation);

                    var dateStamp = DateTime.Now;
                    var dateStampAsString = $"{dateStamp:yyyy-MM-dd}";
                    var logFile = Path.Combine(_logFileLocation, $"{dateStampAsString}.log");
                    await File.AppendAllTextAsync(logFile, bloatedLog + "\n");
                    break;

                default:
                    break;
            }
        }
    }
}