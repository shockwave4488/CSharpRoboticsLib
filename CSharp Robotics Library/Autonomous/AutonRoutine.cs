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
    public class AutonRoutine : Queue<AutonCommand>
    {
        private DateTime timeOut;

        public event Action<AutonCommand> CommandFinished;
        public event Action<AutonCommand> CommandTimedOut;
        public event Action<AutonCommand, double> Periodic;
        public event Action SequenceFinished;

        /// <summary>
        /// Returns true if all commands have finished.
        /// </summary>
        public bool finished => Count == 0;

        /// <summary>
        /// Creates a new scheduler with the specified commands
        /// </summary>
        /// <param name="_commands">commands to be contained by the scheduler</param>
        public AutonRoutine(IEnumerable<AutonCommand> _commands) : base(_commands)
        {
            setTimeOut();
        }

        /// <summary>
        /// Creates a new, empty scheduler
        /// </summary>
        public AutonRoutine() : base()
        {
            setTimeOut();
        }

        /// <summary>
        /// Runs the scheduler, executing the current command.
        /// </summary>
        public void Run()
        {
            double progress = 0;
            if (!finished && ((progress = Peek().Execute()) >= 1 || timeOut < DateTime.Now))
            {
                if (DateTime.Now > timeOut)
                    CommandTimedOut(Peek());
                else
                    CommandFinished(Peek());
                Dequeue();
                setTimeOut();
                Periodic(Peek(), progress);

                if (finished)
                    SequenceFinished();
            }
        }

        private void setTimeOut()
        {
            if(!finished)
                timeOut = DateTime.Now.AddSeconds(Peek().TimeOut);
        }

        private bool TimedOut()
        {
            if (Peek().TimeOut < 0)
                return false;
            else return Peek().TimeOut > 0 && DateTime.Now > timeOut;
        }
    }
}
