using Alge.Domain.Interfaces.Services;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;

namespace Alge.Domain.Services
{
    public class ConnectionService : IConnectionService
    {

        private X509Certificate Certificate { get; set; }
        private List<X509Certificate> CertificateChain { get; set; }

        public (X509Certificate certificate, List<X509Certificate> chain) LoadCertificates(string hostname, int port)
        {
            using (TcpClient tcpClient = new TcpClient(hostname, port))
            using (SslStream sslStream = new SslStream(tcpClient.GetStream(), true, CertificateValidationCallBack))
            {
                sslStream.AuthenticateAsClient(hostname, null, System.Security.Authentication.SslProtocols.Tls12, false);
            }
            return (Certificate, CertificateChain);
        }

        private bool CertificateValidationCallBack(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            CertificateChain = new List<X509Certificate>();
            Certificate = DotNetUtilities.FromX509Certificate(certificate);
            foreach (var cert in chain.ChainElements)
            {
                CertificateChain.Add(DotNetUtilities.FromX509Certificate(cert.Certificate));
            }
            return true;
        }
    }
}
