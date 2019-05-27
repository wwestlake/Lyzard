/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
