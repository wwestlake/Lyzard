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
namespace Lyzard.Simulation
{
    /// <summary>
    /// Provides an abstraction over the computation of the initial conditions
    /// for a simulation.  This may be the initial conditions of a differential 
    /// equation, but may be any type or conditions appropriate to the simulation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class InitialConditions<T>
    {
        /// <summary>
        /// Performs what every is necessary to provide the simulation
        /// with a set of initial conditions T[]
        /// </summary>
        /// <param name="args">Any parameters needed to compute the initial conditions</param>
        /// <returns>The initial conditions</returns>
        public abstract T[] InitialCondition(params T[] args);
    }
}