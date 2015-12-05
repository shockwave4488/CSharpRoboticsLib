using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace CSharpRoboticsLib.Autonomous
{
    public class SafeAction<Targ>
    {
        private ParameterizedThreadStart m_action;
        private Stopwatch m_timer;
        private double m_timeout;

        public SafeAction(ParameterizedThreadStart action, double timeout = 15)
        {
            m_action = action;
            m_timer = new Stopwatch();
            m_timeout = timeout;
        }
        
        public bool Run(Targ param)
        {
            Thread t = new Thread(m_action);
            m_timer.Restart();
            t.Start(param);

            while (m_timer.ElapsedTicks < m_timeout * Stopwatch.Frequency)
            {
                if (!t.IsAlive)
                    return true;
            }

            t.Abort();
            return false;
        }
    }
}
