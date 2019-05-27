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
using System.Collections.Generic;
using System.Reflection;

namespace Lyzard.Compiler
{
    public class Results
    {
        public Results()
        {
            Errors = new List<Error>();
        }

        public Assembly Assembly { get; set; }
        public IList<Error> Errors { get; set; }

        public string PathToAssembly { get; set; }

        public bool HasErrors { get { return Errors.Count > 0; } }

    }
}
