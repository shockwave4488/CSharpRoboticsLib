using System;
using System.Collections.Generic;
using System.IO;
using WPILib.SmartDashboard;

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
                return $"[ {m_timestamp.Minutes} : {m_timestamp.Seconds}.{m_timestamp.Milliseconds} ]\t" + m_message;
            }
        }

        private static DateTime _offset;
        private static List<TimeStampedMessage> _messages;

        static Logger()
        {
            SmartDashboardVariable = null;
            _offset = DateTime.Now;
            _messages = new List<TimeStampedMessage>();
            AddMessage("Logger Initialized");
        }

        /// <summary>
        /// <see cref="SmartDashboard"/> variable name to automatically write to
        /// </summary>
        public static string SmartDashboardVariable { get; set; }

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
        public static void AddMessage(string message)
        {
            _messages.Add(new TimeStampedMessage(message));
            UpdateSmartDashboard();
        }

        /// <summary>
        /// Returns the contents of the <see cref="Logger"/>
        /// </summary>
        /// <returns></returns>
        public new static string ToString()
        {
            string toReturn = "";
            foreach (TimeStampedMessage t in _messages)
            {
                toReturn += t + "\n";
            }
            return toReturn;
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
            if (null != SmartDashboardVariable)
                SmartDashboard.PutString(SmartDashboardVariable, ToString());
        }
    }
}
