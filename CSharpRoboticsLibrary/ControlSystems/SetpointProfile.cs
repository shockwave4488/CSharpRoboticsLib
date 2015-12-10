using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.ControlSystems
{
    public class SetpointProfile : List<Setpoint>, IPIDController
    {
        public double SetPoint { get; set; }

        public SetpointProfile()
        {
            SetPoint = 0;

        }

        private Setpoint NearestPrevious(double currentPoint)
        {
            Setpoint toReturn = this.First();
            foreach (Setpoint p in this)
            {
                if (p.Point > currentPoint && p.Point < toReturn.Point)
                    toReturn = p;
            }
            return toReturn;
        }

        private Setpoint NearestNext(double currentPoint)
        {
            Setpoint toReturn = this.First();
            foreach (Setpoint p in this)
            {
                if (p.Point < currentPoint && p.Point > toReturn.Point)
                    toReturn = p;
            }
            return toReturn;
        }

        public double Get(double currentPoint)
        {
            //be warned: here be lots of math
            double ratioComplete = (currentPoint - NearestPrevious(currentPoint).Point) / (NearestNext(currentPoint).Point - NearestPrevious(currentPoint).Point);
            double currentDelta = ratioComplete*(NearestNext(currentPoint).Value - NearestPrevious(currentPoint).Value);
            return currentDelta + NearestPrevious(currentPoint).Value * Math.Sign(SetPoint - currentPoint);
        }
    }
}
