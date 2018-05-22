using Alge.Domain.Interfaces.Infastructure;

namespace Alge.Domain.Infastructure
{
    public class Settings : ISettings
    {
        public string Version { get; }
        public string PublishDate { get; }
        public string CSP { get; }
        public string LogDirectory { get; }

        public Settings(string version, string publishDate, string csp, string logDirectory)
        {
            Version = version;
            PublishDate = publishDate;
            CSP = csp;
            LogDirectory = logDirectory;
        }
    }
}
