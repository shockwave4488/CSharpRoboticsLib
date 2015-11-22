using System.Collections.Generic;
using WPILib.SmartDashboards;
using WPILib.Interfaces;

namespace CSharpRoboticsLib.Autonomous
{
    public class AutonSelector<T> : Dictionary<T, AutonRoutine> where T : INamedSendable
    {
        private T defaultKey;

        /// <summary>
        /// Smart Dashbaord variable name to read the Autonomous Routine from
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Creates a new AutonSelector with the default key "AutonMode"
        /// </summary>
        public AutonSelector() : this("AutonMode") { }

        /// <summary>
        /// Creates a new AutonSelector class with a specified Dashboard Variable Name
        /// </summary>
        /// <param name="Key_">Dashboard Variable Key</param>
        public AutonSelector(string Key_) : this(Key_, default(T)) { }

        /// <summary>
        /// Creates a new AutonSelector class with a specified Dashboard Variable name assigned to a value, for default purposes
        /// </summary>
        /// <param name="Key_">Dashboard Variable key</param>
        /// <param name="defaultValue">Value to set to the key</param>
        public AutonSelector(string Key_, T defaultValue)
        {
            Key = Key_;
            defaultKey = defaultValue;
            SmartDashboard.PutData(defaultValue);
        }

        /// <summary>
        /// Gets the AutonRoutine associated with the Smart Dashboard value
        /// </summary>
        /// <returns>AutonRoutine</returns>
        public AutonRoutine Get()
        {
            T value = (T)SmartDashboard.GetData(Key);

            try
            {
                return this[value];
            }
            catch(KeyNotFoundException e)
            {
                return this[defaultKey];
            }
        }
    }
}
