using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_Robotics_Library.FlowControl;

namespace CSharp_Robotics_Library.Autonomous
{
    /// <summary>
    /// Simpler command-based autonomous framework.
    /// </summary>
    public class AutonScheduler : Queue<AutonCommand>
    {
        private DateTime timeOut;
        private EdgeTrigger FinishedTrigger;

        public event Action<AutonCommand> CommandFinished;
        public event Action<AutonCommand> CommandTimedOut;
        public event Action<AutonCommand> Periodic;
        public event Action SequenceFinished;

        /// <summary>
        /// Returns true if all commands have finished.
        /// </summary>
        public bool finished => Count == 0;

        /// <summary>
        /// Creates a new scheduler with the specified commands
        /// </summary>
        /// <param name="_commands">commands to be contained by the scheduler</param>
        public AutonScheduler(IEnumerable<AutonCommand> _commands) : base(_commands)
        {
            setTimeOut();
        }

        /// <summary>
        /// Creates a new, empty scheduler
        /// </summary>
        public AutonScheduler() : base()
        {
            setTimeOut();
        }

        /// <summary>
        /// Runs the scheduler, executing the current command.
        /// </summary>
        public void Run()
        {
            if (!finished && (Peek().Execute() || timeOut < DateTime.Now))
            {
                if (DateTime.Now > timeOut)
                    CommandTimedOut(Peek());
                else
                    CommandFinished(Peek());
                Dequeue();
                setTimeOut();
                Periodic(Peek());
            }
            else if (FinishedTrigger.GetRisingUpdate(finished))
                SequenceFinished();
        }

        private void setTimeOut()
        {
            timeOut = DateTime.Now.AddSeconds(Peek().TimeOut);
        }
    }
}
