using Alge.Domain.Enums;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Patterns.OCSP
{
    public abstract class ValidatorBase
    {
        protected ValidatorBase Successor { get; set; }
        protected IList<KeyValuePair<CertificateValidationRules, string>> Errors { get; set; }

        protected abstract CertificateValidationRules Rule { get; }

        protected ValidatorBase()
        {
            Errors = new List<KeyValuePair<CertificateValidationRules, string>>();
        }

        public abstract IList<KeyValuePair<CertificateValidationRules, string>> HandleValidation(X509Certificate certificate, X509Certificate issuer, BasicOcspResp response);

        public void SetSuccessor(ValidatorBase successor)
        {
            this.Successor = successor;
        }

        protected void RecordError(string error)
        {
            this.Errors.Add(new KeyValuePair<CertificateValidationRules, string>(Rule, error));
        }
    }
}
