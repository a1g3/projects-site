using Alge.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace Alge.SignalR
{
    public class CertStreamClient : ICertStreamClient
    {
        private int CertificateCount { get; set; }
        public IHubContext<CertStreamHub> CertStreamContext { get; set; }

        public CertStreamClient(IHubContext<CertStreamHub> hubContext)
        {
            this.CertStreamContext = hubContext;
        }

        public void IncrementCertCount()
        {
            CertificateCount++;
            CertStreamContext.Clients.All.SendAsync("OnCertificateCountUpdate", CertificateCount);
        }
    }
}
