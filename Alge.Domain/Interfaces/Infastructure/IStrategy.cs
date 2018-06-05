namespace Alge.Domain.Interfaces.Infastructure
{
    public interface IStrategy<TStrategies>
    {
        bool AppliesTo(TStrategies strategies);
    }
}
