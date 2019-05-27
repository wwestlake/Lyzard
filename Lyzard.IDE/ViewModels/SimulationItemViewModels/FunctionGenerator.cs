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
using Lyzard.FsMath;
using System;
using System.Collections.Generic;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{

    internal class FunctionGenerator : ViewModelBase
    {
        private string _name;

        /// <summary>
        /// Constructs a FunctionGenerator
        /// </summary>
        public FunctionGenerator()
        {
        }

        /// <summary>
        /// Constructs a FunctionGenerator
        /// </summary>
        public FunctionGenerator(Generators.FunctionGenerator generator)
        {
            Generator = generator;
        }


        /// <summary>
        /// The Name of the Function Generator
        /// </summary>
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        public Generators.FunctionGenerator Generator { get; set; }



        /// <summary>
        /// THe output of the Generator
        /// </summary>
        public IEnumerable<Tuple<double, double>> Output
        {
            get
            {
                foreach (var result in Generator.Generate())
                    yield return result;
            }
        }
    }

    internal class SquareWaveGenerator : FunctionGenerator
    {
        public SquareWaveGenerator()
        {
            Generator = new Generators.SquareWaveGenerator();
        }
    }

    internal class SineWaveGenerator : FunctionGenerator
    {
        public SineWaveGenerator()
        {
            Generator = new Generators.SineWaveGenerator();
        }
    }

    internal class TriangleWaveGenerator : FunctionGenerator
    {
        public TriangleWaveGenerator()
        {
            Generator = new Generators.TriangleWaveGenerator();
        }
    }

    internal class RandomWaveGenerator : FunctionGenerator
    {
        public RandomWaveGenerator()
        {
            Generator = new Generators.RandomWaveGenerator();
        }
    }

}
