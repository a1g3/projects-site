using Alge.Domain.Enums;
using Alge.Domain.Interfaces.Services;
using Alge.Domain.Patterns.Ocsp;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Services
{
    public class CertificateValidationService : ICertificateValidationService
    {

        public IList<KeyValuePair<CertificateValidationRules, string>> ValidateOcspResponse(X509Certificate certificate, X509Certificate issuer, BasicOcspResp response)
        {
            ValidateCertificateId validateCertificateId = new ValidateCertificateId();
            ValidateSignature validateSignature = new ValidateSignature();
            validateCertificateId.SetSuccessor(validateSignature);
            ValidateUpdateTimes validateUpdateTimes = new ValidateUpdateTimes();
            validateSignature.SetSuccessor(validateUpdateTimes);

            return validateCertificateId.HandleValidation(certificate, issuer, response);
        }
    }
}
