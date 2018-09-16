using Alge.Domain.Dtos;

namespace Alge.Domain.Interfaces.Facades
{
    public interface IOcspFacade
    {
        OcspDto GetStatus(string hostname, int port);
    }
}
