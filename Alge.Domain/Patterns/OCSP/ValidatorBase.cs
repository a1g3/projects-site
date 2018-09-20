using Alge.Domain.Enums;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Patterns.Ocsp
{
    public abstract class ValidatorBase
    {
        protected ValidatorBase Successor { get; set; }
        protected IList<(CertificateValidationRules CertificateValidationRule, string Description)> Errors { get; set; }

        protected abstract CertificateValidationRules Rule { get; }

        protected ValidatorBase()
        {
            Errors = new List<(CertificateValidationRules CertificateValidationRule, string Description)>();
        }

        public abstract IList<(CertificateValidationRules CertificateValidationRule, string Description)> HandleValidation(X509Certificate certificate, X509Certificate issuer, BasicOcspResp response);

        public void SetSuccessor(ValidatorBase successor)
        {
            this.Successor = successor;
        }

        protected void RecordError(string error)
        {
            this.Errors.Add((Rule, error));
        }
    }
}
