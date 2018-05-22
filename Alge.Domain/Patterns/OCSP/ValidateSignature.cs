using Alge.Domain.Enums;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System.Collections.Generic;
using System.Linq;

namespace Alge.Domain.Patterns.OCSP
{
    public class ValidateSignature : ValidatorBase
    {
        protected override CertificateValidationRules Rule => CertificateValidationRules.ValidateSignature;

        public override IList<KeyValuePair<CertificateValidationRules, string>> HandleValidation(X509Certificate certificate, X509Certificate issuer, BasicOcspResp response)
        {
            if (!response.Verify(issuer.GetPublicKey()))
            {
                //Check to see if a certificate was sent back by the response
                var recievedCertificate = response.GetCerts()[0];
                if (recievedCertificate != null && response.Verify(recievedCertificate.GetPublicKey()))
                {
                    if (!ValidateAlternateSigner(recievedCertificate, issuer))
                        RecordError("The desginated OCSP signer did not sign the repsonse!");
                }
            } else
            {
                RecordError("The issuer signature does not match the responder signature!");
            }
            if (Successor != null)
                return Successor.HandleValidation(certificate, issuer, response);
            return Errors;
        }

        private bool ValidateAlternateSigner(X509Certificate certificate, X509Certificate issuer)
        {
            byte[] bytes = certificate.GetExtensionValue(new DerObjectIdentifier(X509Extensions.ExtendedKeyUsage.Id)).GetOctets();
            Asn1InputStream aIn = new Asn1InputStream(bytes);
            var As1 = aIn.ReadObject();
            ExtendedKeyUsage extendedKeyUsage = ExtendedKeyUsage.GetInstance(As1);
            var extendedKey = extendedKeyUsage.GetAllUsages().Cast<DerObjectIdentifier>().ToList();
            if (extendedKey.Any(x => x.Id == "1.3.6.1.5.5.7.3.9"))
            {
                certificate.Verify(issuer.GetPublicKey());
                return true;
            }
            return false;
        }
    }
}
