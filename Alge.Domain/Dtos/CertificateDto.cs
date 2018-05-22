namespace Alge.Domain.Dtos
{
    public class CertificateDto
    {
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public string SerialNumber { get; set; }
        public string ExpirationDate { get; set; }
    }
}
