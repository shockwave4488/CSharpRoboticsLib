using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using WPILib.SmartDashboards;

namespace CSharp_Robotics_Library.Autonomous
{
    /// <summary>
    /// Class that reports the autonoumous routine running. Meant to be used with the AutonScheduler class.
    /// </summary>
    public class AutonReporter
    {
        private string title;
        private List<string> report;
        private string currentLine;

        private int countFinished, countTimeOut;
        
        /// <summary>
        /// Dashboard Variable Name to write the report to
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Creates a new, untitled AutonReporter
        /// </summary>
        /// <param name="key">Dashboard Variable Name to write the report to</param>
        public AutonReporter(string key) : this(key, "Untitled Autonomous Routine Report:") { }

        /// <summary>
        /// Creates a new AutonReporter with a title
        /// </summary>
        /// <param name="key">Dashboard Variable Name to write the report to</param>
        /// <param name="_title">Title to be wrote in place of the untitled message</param>
        public AutonReporter(string key, string _title)
        {
            Key = key;
            report = new List<string>();
            title = _title;
            countFinished = countTimeOut = 0;
        }

        /// <summary>
        /// Subscribe this method to an instance of AutonScheduler.CommandFinished
        /// </summary>
        /// <param name="a">command to write</param>
        public void OnCommandFinished(AutonCommand a)
        {
            currentLine = a.ToString() + "...Finished";
            NewLine();
            countFinished++;
        }

        /// <summary>
        /// Subscribe this method to an instance of AutonScheduler.CommandTimedOut
        /// </summary>
        /// <param name="a"></param>
        public void OnCommandTimedOut(AutonCommand a)
        {
            currentLine = a.ToString() + "...Timed Out";
            NewLine();
            countTimeOut++;
        }

        /// <summary>
        /// Subscribe this method to an instance of AutonScheduler.SequenceFinished
        /// </summary>
        public void OnSequenceFinisned()
        {
            currentLine += $"Sequence Complete. Commands Completed Successfully: {countFinished}/{countFinished + countTimeOut}. Commands Timed Out: {countTimeOut}/{countFinished + countTimeOut}";
        }

        /// <summary>
        /// Adds the current line to the report then clears the line
        /// </summary>
        private void NewLine()
        {
            report.Add(currentLine);
            currentLine = "";
            Update();
        }

        /// <summary>
        /// Sends the report to the smart dashboard
        /// </summary>
        private void Update()
        {
            string toSend = title + "\n\n";

            foreach (string s in report)
                toSend += s + "\n";

            toSend += currentLine;

            SmartDashboard.PutString(Key, toSend);
        }
    }
}
