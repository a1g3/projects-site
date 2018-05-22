using Alge.Domain.Dtos;
using Alge.Domain.Interfaces.Facades;
using Alge.Domain.Interfaces.Services;
using Alge.Interfaces.Services;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace Alge.Domain.Facades
{
    public class OCSPFacade : IOCSPFacade
    {
        public IOCSPService OcspService { get; set; }
        public IConnectionService ConnectionService { get; set; }
        public ICertificateValidationService CertificateValidationService { get; set; }

        public OCSPDto GetStatus(string hostname, int port)
        {
            X509Certificate certificate;
            List<X509Certificate> chain;
            try
            {
                (certificate, chain) = ConnectionService.LoadCertificates(hostname, port);
            } catch
            {
                return new OCSPDto() { Status = Enums.OCSPCertificateStatus.Unknown };
            }

            var req = OcspService.CreateOCSPReq(certificate, chain[1]);
            var resp = OcspService.GetOCSPStatus(req);
            var status = OcspService.ParseOCSPResponse(resp);
            var x509Certificate = DotNetUtilities.ToX509Certificate(certificate);

            status.Errors = CertificateValidationService.ValidateOCSPResponse(certificate, chain[1], resp);
            status.Hostname = hostname;
            status.Certificate = new CertificateDto(){ Subject = x509Certificate.Subject, Issuer = x509Certificate.Issuer, SerialNumber = x509Certificate.GetSerialNumberString(), ExpirationDate = x509Certificate.GetExpirationDateString() };

            return status;
        }
    }
}
