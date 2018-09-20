using Alge.Domain.Enums;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Interfaces.Services
{
    public interface ICertificateValidationService
    {
        IList<(CertificateValidationRules CertificateValidationRule, string Description)> ValidateOcspResponse(X509Certificate certificate, X509Certificate issuer, BasicOcspResp response);
    }
}
