using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using WPILib.SmartDashboard;
using System.Runtime.CompilerServices;

namespace CSharpRoboticsLib.Utility
{
    /// <summary>
    /// Static class that can log all information to a text file or SmartDashboard variable
    /// </summary>
    public static class Logger
    {
        private struct TimeStampedMessage
        {
            private TimeSpan m_timestamp;
            private string m_message;

            public TimeStampedMessage(string message)
            {
                m_message = message;
                m_timestamp = DateTime.Now - _offset;
            }

            public override string ToString()
            {
                return $"[ {m_timestamp.Minutes}:{m_timestamp.Seconds}.{m_timestamp.Milliseconds} ]\t" + m_message;
            }
        }

        private static DateTime _offset;
        private static List<TimeStampedMessage> _messages;

        static Logger()
        {
            SmartDashboardName = null;
            PrintToConsole = false;
            ShowDetails = false;
            Level = -1;
            _offset = DateTime.Now;
            _messages = new List<TimeStampedMessage>();
            AddMessage("Logger Initialized");
        }

        /// <summary>
        /// The minimum logger level required to actually log the data - any messages lower leveled than this will be ignored
        /// </summary>
        public static int Level { get; set; }

        /// <summary>
        /// Print the <see cref="Logger.AddMessage"/> caller, source file, and line number along with the message
        /// </summary>
        public static bool ShowDetails { get; set; }

        /// <summary>
        /// Set to true to print logs to the NetConsole window
        /// </summary>
        public static bool PrintToConsole { get; set; }

        /// <summary>
        /// <see cref="SmartDashboard"/> variable name to automatically write to
        /// </summary>
        public static string SmartDashboardName { get; set; }

        /// <summary>
        /// Resets the time value to zero
        /// </summary>
        public static void ResetTimer()
        {
            AddMessage("Timer Reset");
            _offset = DateTime.Now;
        }

        /// <summary>
        /// Adds a message at the current time
        /// </summary>s
        /// <param name="message"></param>
        public static void AddMessage(string message, int messageLevel = 0,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string SourceFilePath = "",
            [CallerLineNumber] int SourceLineNumber = 0)
        {
            if (messageLevel < Level)
                return;

            if (ShowDetails)
                message += $"\t[ From {memberName} in {SourceFilePath} at {SourceLineNumber} ]";

            TimeStampedMessage toAdd = new TimeStampedMessage(message);

            if (PrintToConsole)
                Console.WriteLine(toAdd);

            _messages.Add(toAdd);
            UpdateSmartDashboard();
        }

        /// <summary>
        /// Returns the contents of the <see cref="Logger"/>
        /// </summary>
        /// <returns></returns>
        public new static string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (TimeStampedMessage t in _messages)
            {
                s.Append(t);
            }
            return s.ToString();
        }

        /// <summary>
        /// Saves the log as a text file
        /// </summary>
        /// <param name="filePath"></param>
        public static void Save(string filePath)
        {
            //I have no idea if this will work on the roborio.
            File.WriteAllText(filePath, ToString());
        }

        /// <summary>
        /// Clears all messages
        /// </summary>
        public static void Clear()
        {
            _messages.Clear();
        }

        /// <summary>
        /// Updates the related <see cref="SmartDashboard"/> variable if applicable
        /// </summary>
        public static void UpdateSmartDashboard()
        {
            if (null != SmartDashboardName)
                SmartDashboard.PutString(SmartDashboardName, ToString());
        }
    }
}
