﻿using System.Threading;
using System.Diagnostics;

namespace CSharpRoboticsLib.Autonomous
{
    public class SafeAction
    {
        private ThreadStart m_action;
        private Stopwatch m_timer;
        private double m_timeout;

        public SafeAction(ThreadStart action, double timeOut = 15)
        {
            m_action = action;
            m_timeout = timeOut;
            m_timer = new Stopwatch();
        }

        public bool Run()
        {
            Thread t = new Thread(m_action);
            m_timer.Restart();
            t.Start();

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