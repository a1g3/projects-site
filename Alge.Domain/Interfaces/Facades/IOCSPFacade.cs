using Alge.Domain.Dtos;

namespace Alge.Domain.Interfaces.Facades
{
    public interface IOCSPFacade
    {
        OCSPDto GetStatus(string hostname, int port);
    }
}
