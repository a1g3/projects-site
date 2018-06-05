using System.Collections.Generic;
using Alge.Domain.Enums;
using Alge.Domain.Interfaces.Infastructure;
using Alge.Interfaces.Services;

namespace Alge.Domain.Interfaces
{
    public interface ITlsStrategyChooser : IStrategyChooser<Tls, ITlsScanService>
    {
        IList<string> Scan(string hostname);
    }
}
