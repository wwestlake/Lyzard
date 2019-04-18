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