using Alge.Domain.Enums;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;

namespace Alge.Domain.Patterns.Ocsp
{
    public class ValidateUpdateTimes : ValidatorBase
    {
        protected override CertificateValidationRules Rule => CertificateValidationRules.ValidateUpdateTimes;

        public override IList<(CertificateValidationRules CertificateValidationRule, string Description)> HandleValidation(X509Certificate certificate, X509Certificate issuer, BasicOcspResp response)
        {
            var singleResp = response.Responses[0];
            if (singleResp.NextUpdate == null || singleResp.NextUpdate.Value <= DateTime.Now)
                RecordError("Invalid NextUpdate value!");
            if ((singleResp.ThisUpdate - DateTime.Now.AddDays(-7)).TotalDays > 7)
                RecordError("Invalid ThisUpdate value!");

            if (Successor != null)
                return Successor.HandleValidation(certificate, issuer, response);
            return Errors;
        }
    }
}
