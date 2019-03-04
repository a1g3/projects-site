using System;
using System.Collections.Generic;
using System.Text;

namespace Alge.Domain.Interfaces.Patterns.SignalR
{
    public interface ICertStreamClient
    {
        void UpdateCertificateCount(int certCount);
    }
}
