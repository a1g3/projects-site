using Alge.Domain.Interfaces.Infastructure;

namespace Alge.Domain.Interfaces.Strategy
{
    public abstract class Strategy<TStrategy> : IStrategy<TStrategy>
    {
        public abstract TStrategy StrategyType { get; }

        public bool AppliesTo(TStrategy strategies)
        {
            return StrategyType.Equals(strategies);
        }
    }
}
