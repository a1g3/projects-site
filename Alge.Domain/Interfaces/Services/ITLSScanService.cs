using System.Collections.Generic;

namespace Alge.Interfaces.Services
{
    public interface ITlsScanService
    {
        List<string> Scan(string hostname);
    }
}
