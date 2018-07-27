using System.Collections.Generic;
using Alge.Domain.Enums;
using Alge.Domain.Interfaces;
using Alge.Domain.Interfaces.Strategy;
using Alge.Interfaces.Services;

namespace Alge.Domain.Services
{
    public class TlsStrategyChooser : StrategyChooser<Tls, ITlsScanService>, ITlsStrategyChooser
    {
        public TlsStrategyChooser(List<ITlsScanService> strategies) : base(strategies) { }
        
        public IList<CipherSuites> Scan(string hostname)
        {
            return this.Service.Scan(hostname);
        }
    }
}
