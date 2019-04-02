using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Core
{
    /// <summary>
    /// Represents a branch operation following the "truePath" only if the condition input it true,
    /// otherwise it follows the "falsePath"
    /// </summary>
    public class Branch : BaseExecutionBlock
    {
        private BaseExecutionBlock _truePath;
        private BaseExecutionBlock _falsePath;

        /// <summary>
        /// Constructs a Branch execution block
        /// </summary>
        /// <param name="truePath">The path to follow when the condition is true</param>
        /// <param name="falsePath">The path to follow when the condition is false</param>
        public Branch(BaseExecutionBlock truePath, BaseExecutionBlock falsePath)
        {
            _enableNext = false;
            AddInput<bool>("Condition", true);
            _truePath = truePath;
            _falsePath = falsePath;
        }

        /// <summary>
        /// Runs the operation
        /// </summary>
        protected override void Operation()
        {
            var condition = GetValue<bool>("Condition");
            if (condition)
            {
                _truePath.Execute();
            } else
            {
                _falsePath.Execute();
            }
        }
    }
}
