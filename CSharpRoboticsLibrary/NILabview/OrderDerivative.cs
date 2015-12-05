using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Describes a Higher Order derivative function
    /// </summary>
    class OrderDerivative
    {
        private Queue<double> m_values;
        private DateTime m_dt;
        private int _order => m_values.Count - 1;

        /// <summary>
        /// Set the order of the derivative function. 
        /// Values will be dequeued to shrink, and enqueued with the same value as the current last to increase size.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                if (_order == value || Order == 0)
                    return;
                else if (_order < value)
                {
                    m_values.Dequeue();
                    Order = value;
                }
                else if (_order > value)
                {
                    m_values.Enqueue(m_values.Last());
                    Order = value;
                }
            }
        }

        /// <summary>
        /// Creates a new OrderDeritative class.
        /// </summary>
        /// <param name="initialValue">Initial value of the derivative function</param>
        /// <param name="order">Order value of the derivative</param>
        public OrderDerivative(double initialValue, int order)
        {
            m_values = new Queue<double>();
            m_values.Enqueue(initialValue);
            Order = order;
            m_dt = DateTime.Now;
        }

        /// <summary>
        /// Updates the derivative function with a new value. Call this after the Get() Method.
        /// </summary>
        /// <param name="x">new value of x</param>
        public void Update(double x)
        {
            m_values.Enqueue(x);
            m_values.Dequeue();
            m_dt = DateTime.Now;
        }

        /// <summary>
        /// Gets dx/dt 
        /// </summary>
        /// <returns>dx/dt</returns>
        public double Get()
        {
            return (m_values.First() - m_values.Last()) / ((DateTime.Now - m_dt).TotalSeconds * _order);
        }
    }
}
