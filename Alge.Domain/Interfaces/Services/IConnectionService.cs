using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Interfaces.Services
{
    public interface IConnectionService
    {
        (X509Certificate certificate, List<X509Certificate> chain) LoadCertificates(string hostname, int port);
    }
}
