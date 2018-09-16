using Alge.Domain.Dtos;
using System;

namespace Alge.ViewModels
{
    public class OcspStatusViewModel
    {
        public int Status { get; set; }
        public DateTime RevocationTime { get; set; }
        public int RevocationReason { get; set; }
        public DateTime ProducedAt { get; set; }
        public DateTime ThisUpdate { get; set; }
        public DateTime NextUpdate { get; set; }
        public string Error { get; set; }
        public CertificateDto Certificate { get; set; }
    }
}
