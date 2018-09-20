using Alge.Domain.Dtos;
using Alge.Domain.Enums;
using Alge.Domain.Interfaces.Facades;
using Alge.Domain.Interfaces.Services;
using Alge.Interfaces.Services;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Facades
{
    public class OcspFacade : IOcspFacade
    {
        public IOcspService OcspService { get; set; }
        public IConnectionService ConnectionService { get; set; }
        public ICertificateValidationService CertificateValidationService { get; set; }

        public OcspDto GetStatus(string hostname, int port)
        {
            X509Certificate certificate;
            List<X509Certificate> chain;
            OcspDto status = new OcspDto();
            try
            {
                (certificate, chain) = ConnectionService.LoadCertificates(hostname, port);
            } catch
            {
                return new OcspDto() { Status = Enums.OcspCertificateStatus.Unknown };
            }

            var req = OcspService.CreateOcspReq(certificate, chain[1]);
            var resp = OcspService.GetOcspStatus(req);
            
            if(resp != null)
            {
                status = OcspService.ParseOcspResponse(resp);
                status.Errors = CertificateValidationService.ValidateOcspResponse(certificate, chain[1], resp);
            } else
            {
                status.Status = OcspCertificateStatus.Unknown;
            }

            var x509Certificate = DotNetUtilities.ToX509Certificate(certificate);

            status.Hostname = hostname;
            status.Certificate = new CertificateDto(){ Subject = x509Certificate.Subject, Issuer = x509Certificate.Issuer, SerialNumber = x509Certificate.GetSerialNumberString(), ExpirationDate = x509Certificate.GetExpirationDateString() };

            return status;
        }
    }
}
