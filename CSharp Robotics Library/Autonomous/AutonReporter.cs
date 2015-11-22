using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using WPILib.SmartDashboards;

namespace CSharpRoboticsLib.Autonomous
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
        public string ReportKey { get; set; }

        /// <summary>
        /// Dashboard Variable Name to write the Progress of the current command to
        /// </summary>
        public string ProgressKey { get; set; }

        /// <summary>
        /// Creates a new AutonReporter with a title
        /// </summary>
        /// <param name="key">Dashboard Variable Name to write the report to</param>
        /// <param name="_title">Title to be wrote in place of the untitled message</param>
        public AutonReporter(string _title = "Untitled Autonomous Routine:")
        {
            report = new List<string>();
            title = _title;
            countFinished = countTimeOut = 0;
        }

        /// <summary>
        /// Creates a new AutonReporter with a title, automatically linked to the specified routine
        /// </summary>
        /// <param name="key">Dashboard Variable name to write the report to</param>
        /// <param name="_title">Title to begin the report with</param>
        /// <param name="routine">Routine to subscribe methods to</param>
        public AutonReporter(string _title, AutonRoutine routine) : this(_title)
        {
            routine.CommandFinished += OnCommandFinished;
            routine.CommandTimedOut += OnCommandTimedOut;
            routine.SequenceFinished += OnSequenceFinished;
            routine.Periodic += OnSequencePeriodic;
        }

        /// <summary>
        /// Creates a new AutonReporter automatically linked to the specified routine
        /// </summary>
        /// <param name="key">Dashboard Variable name to write the report to</param>
        /// <param name="routine">Routine to subscribe methods to</param>
        public AutonReporter(AutonRoutine routine) : this("Untitled Autonomous Routine:", routine) { }

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
        /// Subscribe this method to an instance of AutonScheduler.Periodic
        /// </summary>
        /// <param name="a"></param>
        /// <param name="progress"></param>
        public void OnSequencePeriodic(AutonCommand a, double progress)
        {
            if (null != ProgressKey)
                SmartDashboard.PutNumber(ProgressKey, progress);
        }

        /// <summary>
        /// Subscribe this method to an instance of AutonScheduler.SequenceFinished
        /// </summary>
        public void OnSequenceFinished()
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

             if(null != ReportKey)
                SmartDashboard.PutString(ReportKey, toSend);
        }
    }
}
