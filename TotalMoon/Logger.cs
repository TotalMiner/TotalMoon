namespace TotalMoon
{
    enum LogType
    {
        ERROR,
        INFO
    }

    static class Logger
    {
        public delegate void LogEvent(LogType type, string msg);
        public static event LogEvent Logged;

        public static void Info(string msg) => Logged(LogType.INFO, msg);
        public static void Error(string msg) => Logged(LogType.ERROR, msg);
    }
}