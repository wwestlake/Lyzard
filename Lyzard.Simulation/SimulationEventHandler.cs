namespace Lyzard.Simulation
{
    public delegate void SimulationEventHandler<T>(SimulationContract<T> sender, SimulationEventArgs<T> e);
}