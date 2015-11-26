using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Describes a Higher Order derivative function
    /// </summary>
    public class OrderDerivative
    {
        private Queue<double> values;
        private DateTime dt;
        private int _order => values.Count - 1;

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
                    values.Dequeue();
                    Order = value;
                }
                else if (_order > value)
                {
                    values.Enqueue(values.Last());
                    Order = value;
                }
            }
        }

        /// <summary>
        /// Creates a new OrderDeritative class.
        /// </summary>
        /// <param name="InitialValue">Initial value of the derivative function</param>
        /// <param name="order">Order value of the derivative</param>
        public OrderDerivative(double InitialValue, int order)
        {
            values = new Queue<double>();
            values.Enqueue(InitialValue);
            Order = order;
            dt = DateTime.Now;
        }

        /// <summary>
        /// Updates the derivative function with a new value. Call this after the Get() Method.
        /// </summary>
        /// <param name="x">new value of x</param>
        public void Update(double x)
        {
            values.Enqueue(x);
            values.Dequeue();
            dt = DateTime.Now;
        }

        /// <summary>
        /// Gets dx/dt 
        /// </summary>
        /// <returns>dx/dt</returns>
        public double Get()
        {
            return (values.First() - values.Last()) / ((DateTime.Now - dt).TotalSeconds * _order);
        }
    }
}
