using System;
using System.Collections.Generic;
using Alge.Domain.Enums;
using Alge.Domain.Interfaces.Strategy;
using Alge.Interfaces.Services;

namespace Alge.Domain.Services
{
    public abstract class TlsSelectionService : Strategy<Tls>, ITlsScanService
    {
        public abstract List<string> Scan(string hostname);
    }

    public class Ssl2ScanService : TlsSelectionService
    {
        public override Tls StrategyType => Tls.SSL2;

        public override List<string> Scan(string hostname)
        {
            throw new NotImplementedException();
        }
    }
}
