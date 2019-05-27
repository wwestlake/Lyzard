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
/* 
 * Lyzard Code Generation System
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

namespace Lyzard.Core
{
    /// <summary>
    /// Abstract base class for execution blocks
    /// </summary>
    public abstract class BaseExecutionBlock : BaseBlock
    {
        private BaseExecutionBlock _next;

        /// <summary>
        /// Returns the next execution block in the chain.
        /// If the next execution block is not rnabled this
        /// returns null and cannot be set.
        /// </summary>
        public  BaseExecutionBlock Next
        {
            get
            {
                if (_enableNext) return _next;
                return null;
            }
            set
            {
                if (_enableNext) _next = value;
            }
        }

        /// <summary>
        /// Returns true is the Next execution block is available to be set
        /// This is used by the UI to determine if a Next execution block
        /// connection should be displayed.
        /// </summary>
        public bool IsNextAvailabe
        {
            get
            {
                return _enableNext;
            }
        }

        /// <summary>
        /// Executes this block
        /// </summary>
        public void Execute()
        {
            Operation();
            Next?.Execute();
        }


        /// <summary>
        /// Override to provide block functionality.
        /// </summary>
        protected abstract void Operation();

        /// <summary>
        /// Set to false by sub classes that do not support the 
        /// </summary>
        protected bool _enableNext = true;
    }
}
