using Lyzard.FsMath;
using System;
using System.Collections.Generic;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{

    public class FunctionGenerator : ViewModelBase
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

    public class SquareWaveGenerator : FunctionGenerator
    {
        public SquareWaveGenerator()
        {
            Generator = new Generators.SquareWaveGenerator();
        }
    }

    public class SineWaveGenerator : FunctionGenerator
    {
        public SineWaveGenerator()
        {
            Generator = new Generators.SineWaveGenerator();
        }
    }

    public class TriangleWaveGenerator : FunctionGenerator
    {
        public TriangleWaveGenerator()
        {
            Generator = new Generators.TriangleWaveGenerator();
        }
    }

    public class RandomWaveGenerator : FunctionGenerator
    {
        public RandomWaveGenerator()
        {
            Generator = new Generators.RandomWaveGenerator();
        }
    }

}
