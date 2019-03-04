namespace Alge.Domain.Patterns.SignalR
{
    public class CertStreamHub : BaseHub
    {
        public CertStreamClient CertStreamClient { get; set; }

        public CertStreamHub(CertStreamClient certStreamClient)
        {
            this.CertStreamClient = certStreamClient;
        }
    }
}
