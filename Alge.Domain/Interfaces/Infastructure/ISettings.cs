namespace Alge.Domain.Interfaces.Infastructure
{
    public interface ISettings
    {
        string Version { get; }
        string PublishDate { get; }
        string CSP { get; }
        string LogDirectory { get; }
    }
}
