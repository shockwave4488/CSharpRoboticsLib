using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.Framework
{
    /// <summary>
    /// Inherit from this class whenever a class has a method that should be called periodically.
    /// </summary>
    public abstract class UpdatingBase
    {
        private List<UpdatingBase> Children;

        /// <summary>
        /// Execute whatever periodic acions should be taken
        /// </summary>
        public virtual void Update()
        {
            for (int i = 0; i < Children.Count; i++)
                Children[i].Update();
        }

        /// <summary>
        /// Basic constructor, call this with the parameter "this". 
        /// example: <see cref="UpdatingBase"/> something = new <see cref="UpdatingBase"/>(this);
        /// </summary>
        /// <param name="caller"></param>
        public UpdatingBase(object caller)
        {
            UpdatingBase Parent = (caller as UpdatingBase);

            if (null == Parent)
                return;
            if (null == Parent.Children) //we wouldn't want a bunch of empty lists running around, would we?
                Parent.Children = new List<UpdatingBase>();

            Parent.Children.Add(this);
        }
    }
}
