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

    public enum ConnectorTypes { Input, Output }

    /// <summary>
    /// Provides a description of a connector
    /// </summary>
    public class ConnectorDescription
    {
        /// <summary>
        /// The name of the connector
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The ConnectortType of this connector
        /// </summary>
        public ConnectorTypes ConnectorType { get; internal set; }

        /// <summary>
        /// The delegate to call
        /// </summary>
        public Delegate Delegate { get; internal set; }

        /// <summary>
        /// The value type of this connector
        /// </summary>
        public Type Type { get; internal set; }
    }
}
