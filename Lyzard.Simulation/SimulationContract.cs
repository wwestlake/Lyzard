using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Simulation
{

    /// <summary>
    /// A simulation is a mathematical representation of some variables 
    /// changing over a squence of operations.  A delta time value is
    /// supplied because many simulations are performed over time.  Other
    /// aruments maybe supplied so long as they are all the same time T or
    /// subtypes of T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SimulationContract<T>
    {
        public event SimulationEventHandler<T> SimulationStart;
        public event SimulationEventHandler<T> SimulationProgress;
        public event SimulationEventHandler<T> SimulationStopping;
        public event SimulationEventHandler<T> SimulationStopped;

        protected SimulationContract<T> SubContract { get; set; }
        protected CompletionCriteria<T> CompletionCriteria { get; set; }
        protected InitialConditions<T> InitialConditions { get; set; }

        protected SimulationContract()
        {
        }

        /// <summary>
        /// Starts a simulation on the curent thread and runs to completion
        /// </summary>
        /// <param name="func"></param>
        /// <param name="args"></param>
        public T[] Start(T[] initialValues)
        {
            SimulationStart?.Invoke(this, new SimulationEventArgs<T>());
            T[] result = InitialConditions.InitialCondition(initialValues);
            var deltaTime = 0.0;
            while (true)
            {
                var iterResult = Iteration(CalculateDeltaTime(ref deltaTime), result);
                SimulationProgress?.Invoke(this, new SimulationEventArgs<T>(iterResult.Item1));

                if (iterResult.Item2.IsComplete(iterResult.Item1))
                {
                    SimulationStopping?.Invoke(this, new SimulationEventArgs<T>(iterResult.Item1));
                    break;
                }
            }
            SimulationStopped?.Invoke(this, new SimulationEventArgs<T>(result));
            return result;
        }

        /// <summary>
        /// Starts a simulation asynchronously and runs to completion
        /// </summary>
        /// <param name="initialValues">The initial values for the simulation</param>
        /// <returns>The results of the simulation</returns>
        public async Task<T[]> StartAsync(T[] initialValues)
        {
            return await Task.Factory.StartNew<T[]>(() => {
                return Start(initialValues);
            });
        }

        private double lastTime = DateTime.Now.Ticks;

        private double CalculateDeltaTime(ref double deltaTime)
        {
            double currentTime = DateTime.Now.Ticks;
            deltaTime = currentTime - lastTime;
            lastTime = currentTime;
            return deltaTime;
        }

        protected abstract Tuple<T[], CompletionCriteria<T>>  Iteration(double deltaTime, params T[] args);



    }
}
