namespace Alge.Domain.Interfaces.Infastructure
{
    public interface IStrategyChooser<TStrategies, TStrategy>
    {
        void SetStrategy(TStrategies strategies);
    }
}
