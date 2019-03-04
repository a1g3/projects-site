using Alge.Domain.Interfaces.Patterns.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Alge.Domain.Patterns.SignalR
{
    public class CertStreamClient : ICertStreamClient
    {
        public CertStreamClient(IHubContext<CertStreamHub> certStreamContext)
        {
            this.CertStreamContext = certStreamContext;
        }

        private IHubContext<CertStreamHub> CertStreamContext { get; }

        public void UpdateCertificateCount(int certCount)
        {
            CertStreamContext.Clients.Group("certstream").SendAsync("OnCertificateCountUpdated", certCount);
        }
    }
}
