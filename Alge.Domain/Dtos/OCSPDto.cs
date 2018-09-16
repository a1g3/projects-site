using Alge.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Alge.Domain.Dtos
{
    public class OcspDto
    {
        public string Hostname { get; set; }
        public CertificateDto Certificate { get; set; }

        public OcspCertificateStatus Status { get; set; }
        public DateTime RevocationTime { get; set; }
        public int RevocationReason { get; set; }
        public DateTime ProducedAt { get; set; }
        public DateTime ThisUpdate { get; set; }
        public DateTime NextUpdate { get; set; }

        public IList<KeyValuePair<CertificateValidationRules, string>> Errors { get; set; }
    }
}
