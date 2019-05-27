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
    /// Represents a base class for all operations that do not require 
    /// and execution thread but only provide input/output operations
    /// </summary>
    public abstract class BaseBlock
    {
        private Dictionary<string, ConnectorDescription> _inputs = new Dictionary<string, ConnectorDescription>();
        private Dictionary<string, ConnectorDescription> _outputs = new Dictionary<string, ConnectorDescription>();


        /// <summary>
        /// Creates a connection from and output to an input of this block
        /// </summary>
        /// <typeparam name="T">The parameter type</typeparam>
        /// <param name="name">The name of the input</param>
        /// <param name="output">The output connector</param>
        public void ConnectToInput<T>(string name, OutputConnector<T> output)
        {
            var connector = _inputs[name];
            connector.Delegate = new InputConnector<T>(() => output());
        }

        /// <summary>
        /// Returns the delegate of the specified output as an
        /// output connector of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of output connector</typeparam>
        /// <param name="name">The name of the output</param>
        /// <returns>An OutputConnector<typeparamref name="T"/></returns>
        public OutputConnector<T> GetOutput<T>(string name)
        {
            return Output(name).Delegate as OutputConnector<T>;
        }

        /// <summary>
        /// Names of all inputs
        /// </summary>
        public IEnumerable<string> InputNames { get { return _inputs.Keys; } }

        /// <summary>
        /// Names of all outputs
        /// </summary>
        public IEnumerable<string> OutputNames { get { return _outputs.Keys; } }

        /// <summary>
        /// Gets an input delegate description
        /// </summary>
        /// <param name="name">The name of the input</param>
        /// <returns>The ConnectorDescription</returns>
        public ConnectorDescription Input(string name)
        {
            return _inputs[name];
        }


        /// <summary>
        /// Gets an output delegate description
        /// </summary>
        /// <param name="name">The name of the output</param>
        /// <returns>The ConnectorDescription</returns>
        public ConnectorDescription Output(string name)
        {
            return _outputs[name];
        }

        /// <summary>
        /// Enumerats all outputs
        /// </summary>
        public IEnumerable<ConnectorDescription> Outputs
        {
            get
            {
                return _outputs.Values;
            }
        }

        /// <summary>
        /// Enumerates all inputputs
        /// </summary>
        public IEnumerable<ConnectorDescription> Inputs
        {
            get
            {
                return _inputs.Values;
            }
        }


        protected void AddInput<T>(string name, T defaultValue = default(T))
        {
            _inputs.Add(name, new ConnectorDescription
            {
                Name = name,
                Delegate = new InputConnector<T>(() => defaultValue),
                ConnectorType = ConnectorTypes.Input,
                Type = typeof(T)
            });
        }

        protected void AddOutput<T>(string name, OutputConnector<T> outputConnector)
        {
            _outputs.Add(name, new ConnectorDescription
            {
                Name = name,
                Delegate = outputConnector,
                ConnectorType = ConnectorTypes.Output,
                Type = outputConnector.Method.GetParameters().FirstOrDefault()?.ParameterType
            });
        }



        protected object GetValue(string inputName)
        {
            var input = _inputs[inputName];
            if (input != null)
            {
                return input.Delegate.DynamicInvoke();
            }
            throw new NullReferenceException($"Connector {inputName} has not been defined");
        }

        protected T GetValue<T>(string inputName)
        {
            return SafeCast<T>(GetValue(inputName));
        }

        protected static T SafeCast<T>(object obj)
        {
            if (obj == null) return default(T);
            if (obj.GetType() == typeof(T) || obj.GetType().IsSubclassOf(typeof(T)))
            {
                return (T)obj;
            }
            return default(T);
        }
            


    }
}
