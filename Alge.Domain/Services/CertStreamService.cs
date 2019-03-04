using Alge.Domain.Interfaces.Patterns.SignalR;
using Alge.Domain.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alge.Domain.Services
{
    public class CertStreamService : IHostedService
    {
        public ICertStreamClient CertStreamClient { get; set; }
        private static int CertCount { get; set; }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return MonitorUpdates(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task MonitorUpdates(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
                await ConnectToStream();
        }

        private async Task ConnectToStream()
        {
            using (ClientWebSocket ws = new ClientWebSocket())
            {
                Uri serverUri = new Uri("wss://certstream.calidog.io/");
                await ws.ConnectAsync(serverUri, CancellationToken.None);
                while (ws.State == WebSocketState.Open)
                {
                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[32768]);
                    WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                    SendMessage(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
                }
            }
        }

        private void SendMessage(string responseText)
        {
            var response = JsonConvert.DeserializeObject<CertStreamResponseModel>(responseText);
            if (response.MessageType.Equals("heartbeat", StringComparison.OrdinalIgnoreCase)) return;
            CertStreamClient.UpdateCertificateCount(++CertCount);
        }
    }
}
