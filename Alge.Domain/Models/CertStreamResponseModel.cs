using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alge.Domain.Models
{
    public class CertStreamResponseModel
    {
        [JsonProperty("message_type")]
        public string MessageType { get; set; }
        [JsonProperty("data")]
        public Log Data { get; set; }
    }

    public class Log
    {
        [JsonProperty("update_type")]
        public string UpdateType { get; set; }
        [JsonProperty("leaf_cert")]
        public Certificate LeafCert { get; set; }
        [JsonProperty("chain")]
        public List<Certificate> Chain { get; set; }
        [JsonProperty("cert_index")]
        public string CertIndex { get; set; }
        [JsonProperty("seen")]
        public double Seen { get; set; }
        [JsonProperty("source")]
        public Source Source { get; set; }
    }

    public class Source
    {
        [JsonProperty("url")]
        public string URL { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Certificate
    {
        [JsonProperty("subject")]
        public CertificateSubject Subject { get; set; }
        [JsonProperty("extensions")]
        public CertificateExtensions Extensions { get; set; }
        [JsonProperty("not_before")]
        public double NotBefore { get; set; }
        [JsonProperty("not_after")]
        public double NotAfter { get; set; }
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }
        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }
        [JsonProperty("as_der")]
        public string AsDER { get; set; }
        [JsonProperty("all_domains")]
        public List<string> Domains { get; set; }
    }

    public class CertificateExtensions
    {
        [JsonProperty("keyUsage")]
        public string KeyUsage { get; set; }
        [JsonProperty("extendedKeyUsage")]
        public string ExtendedKeyUsage { get; set; }
        [JsonProperty("basicConstraints")]
        public string BasicConstraints { get; set; }
        [JsonProperty("subjectKeyIdentifier")]
        public string SubjectKeyIdentifier { get; set; }
        [JsonProperty("authorityKeyIdentifier")]
        public string AuthorityKeyIdentifier { get; set; }
        [JsonProperty("authorityInfoAccess")]
        public string AuthorityInfoAccess { get; set; }
        [JsonProperty("subjectAltName")]
        public string SubjectAltName { get; set; }
        [JsonProperty("certificatePolicies")]
        public string CertificatePolicies { get; set; }
    }

    public class CertificateSubject
    {
        [JsonProperty("aggregated")]
        public string Aggregated { get; set; }
        [JsonProperty("C")]
        public string Country { get; set; }
        [JsonProperty("ST")]
        public string State { get; set; }
        [JsonProperty("L")]
        public string Location { get; set; }
        [JsonProperty("O")]
        public string Organization { get; set; }
        [JsonProperty("OU")]
        public string OrganizationUnit { get; set; }
        [JsonProperty("CN")]
        public string CommonName { get; set; }
    }
}
