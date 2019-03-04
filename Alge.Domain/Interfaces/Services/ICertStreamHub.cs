using System.Threading.Tasks;

namespace Alge.Domain.Interfaces.Services
{
    public interface ICertStreamHub
    {
        Task UpdateCertificateCount(int certNumber);
    }
}
