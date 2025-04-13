namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Defines the <see cref="clsErrorEventLog" />
    /// </summary>
    public class clsErrorEventLog
    {
        /// <summary>
        /// Defines the SourceName
        /// </summary>
        private static readonly string SourceName = "ProjectsRepositoryDB";

        /// <summary>
        /// Initializes static members of the <see cref="clsErrorEventLog"/> class.
        /// </summary>
        static clsErrorEventLog()
        {
            if (OperatingSystem.IsWindows()) // Ensure we're on Windows
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Application");
                }
            }
        }

        /// <summary>
        /// The LogError
        /// </summary>
        /// <param name="message">The message<see cref="string"/></param>
        public static void LogError(string message)
        {
            if (OperatingSystem.IsWindows())
            {
                EventLog.WriteEntry(SourceName, message, EventLogEntryType.Error);
            }
        }
    }
}
