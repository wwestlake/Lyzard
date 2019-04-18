using System;

namespace Lyzard.Simulation
{
    public class SimulationEventArgs<T> : EventArgs
    {
        public SimulationEventArgs()
        {

        }

        public SimulationEventArgs(T[] values)
        {
            Values = values;
        }

        public T[] Values { get; }
    }
}