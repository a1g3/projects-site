using Alge.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Alge.SignalR
{
    public class CertStreamHub : Hub, ICertStreamHub
    {
        public async Task UpdateCertificateCount(int certNumber)
        {
            await Task.CompletedTask;
            //await Clients.All.SendCoreAsync("UpdateCertificateNumber", new object[] { certNumber });
        }
    }
}
