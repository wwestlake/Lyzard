namespace Lyzard.Simulation
{
    public abstract class CompletionCriteria<T>
    {
        public abstract bool IsComplete(params T[] args);
    }
}