using Alge.Domain.Enums;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Patterns.Ocsp
{
    public class ValidateCertificateId : ValidatorBase
    {
        protected override CertificateValidationRules Rule => CertificateValidationRules.ValidateCertificateId;

        public override IList<KeyValuePair<CertificateValidationRules, string>> HandleValidation(X509Certificate certificate, X509Certificate issuer, BasicOcspResp response)
        {
            CertificateID actualCertificateId = response.Responses[0].GetCertID();
            CertificateID expectedId = new CertificateID(CertificateID.HashSha1, issuer, certificate.SerialNumber);

            if (!expectedId.SerialNumber.Equals(actualCertificateId.SerialNumber))
                RecordError("Certificate IDs do not match!");
            if (!Org.BouncyCastle.Utilities.Arrays.AreEqual(expectedId.GetIssuerNameHash(), actualCertificateId.GetIssuerNameHash()))
                RecordError("The issuer names do not match!");

            if (Successor != null)
                return Successor.HandleValidation(certificate, issuer, response);
            return Errors;
        }
    }
}
