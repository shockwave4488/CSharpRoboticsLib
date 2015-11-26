﻿using System;

namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Single-Order derivative function. Tested to have 99% accuracy.
    /// </summary>
    public class Derivative
    {
        private double xPrev1, xPrev2;
        private DateTime dt;

        /// <summary>
        /// Creates a new Derivative object.
        /// </summary>
        /// <param name="InitialCondition">Initial value to set x</param>
        public Derivative(double InitialCondition)
        {
            xPrev1 = xPrev2 = InitialCondition;
            dt = DateTime.Now;
        }

        /// <summary>
        /// Creates a new derivative object with the initial value set to zero.
        /// </summary>
        public Derivative() : this(0) { }

        /// <summary>
        /// Updates the derivative. Call this after Get() has been used.
        /// </summary>
        /// <param name="x">new value of x</param>
        public void Update(double x)
        {
            xPrev2 = xPrev1;
            xPrev1 = x;
            dt = DateTime.Now;
        }

        /// <summary>
        /// Gets dx/dt
        /// </summary>
        /// <returns>dx/dt</returns>
        public double Get()
        {
            double toReturn = (xPrev1 - xPrev2) * TimeSpan.TicksPerSecond / (DateTime.Now - dt).Ticks;
            toReturn = double.IsNaN(toReturn) ? 0 : toReturn; //handling division by zero errors
            return toReturn;
        }
    }
}
