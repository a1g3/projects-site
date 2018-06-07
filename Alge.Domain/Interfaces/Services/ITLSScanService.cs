using System.Collections.Generic;
using Alge.Domain.Enums;

namespace Alge.Interfaces.Services
{
    public interface ITlsScanService
    {
        List<CipherSuites> Scan(string hostname);
        List<byte> Connect(string hostname, byte[] stream);
    }
}
