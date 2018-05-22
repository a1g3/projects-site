using Alge.Domain.Dtos;
using Alge.Domain.Enums;
using Alge.Interfaces.Services;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;

namespace Alge.Domain.Services
{
    public class OCSPService : IOCSPService
    {

        #region Properties

        public X509Certificate Certificate { get; set; }
        public X509Certificate Issuer { get; set; }

        private readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        public OcspReq CreateOCSPReq(X509Certificate certificate, X509Certificate issuer)
        {
            Certificate = certificate;
            Issuer = issuer;
            CertificateID id = new CertificateID(CertificateID.HashSha1, Issuer, Certificate.SerialNumber);
            var gen = new OcspReqGenerator();
            gen.AddRequest(id);

            BigInteger.TryParse(Math.Floor((DateTime.UtcNow - Jan1st1970).TotalSeconds).ToString(), out BigInteger nonce);
            var ext = new Dictionary<DerObjectIdentifier, X509Extension>
            {
                { OcspObjectIdentifiers.PkixOcspNonce, new X509Extension(false, new DerOctetString(nonce.ToByteArray())) }
            };
            gen.SetRequestExtensions(new X509Extensions(ext));

            return gen.Generate();
        }

        public IList<Uri> GetOCSPUris()
        {
            List<Uri> ocspUris;
            try
            {
                byte[] bytes = Certificate.GetExtensionValue(new DerObjectIdentifier(X509Extensions.AuthorityInfoAccess.Id)).GetOctets();
                Asn1InputStream aIn = new Asn1InputStream(bytes);
                var Asn1 = aIn.ReadObject();
                AuthorityInformationAccess authorityInformationAccess = AuthorityInformationAccess.GetInstance(Asn1);
                var ocspValues = authorityInformationAccess.GetAccessDescriptions().Where(x => x.AccessMethod.Id.Equals("1.3.6.1.5.5.7.48.1"));
                ocspUris = ocspValues.Select(x => new Uri(x.AccessLocation.Name.ToString())).ToList();
            }
            catch
            {
                ocspUris = new List<Uri>();
            }
            return ocspUris;
        }

        public BasicOcspResp GetOCSPStatus(OcspReq OCSPrequest)
        {
            byte[] reqArray = OCSPrequest.GetEncoded();
            var uris = GetOCSPUris();
            OcspResp resp;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uris[0]);
                request.Method = "POST";
                var requestStream = request.GetRequestStream();
                request.ContentLength = reqArray.Length;
                request.ContentType = "application/ocsp-request";
                request.Accept = "application/ocsp-response";
                requestStream.Write(reqArray, 0, reqArray.Length);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                {
                    resp = new OcspResp(stream);
                }
                return (BasicOcspResp)resp.GetResponseObject();
            }
            catch
            {
                return null;
            }
        }

        public OCSPDto ParseOCSPResponse(BasicOcspResp brep)
        {
            SingleResp singleResp = brep.Responses[0];
            Object itstatus = singleResp.GetCertStatus();
            OCSPDto status = new OCSPDto()
            {
                ProducedAt = brep.ProducedAt,
                ThisUpdate = singleResp.ThisUpdate,
                NextUpdate = singleResp.NextUpdate.Value
            };

            if (itstatus == CertificateStatus.Good)
                status.Status = OCSPCertificateStatus.Good;
            else if (itstatus is RevokedStatus revokedStatus)
            {
                status.Status = OCSPCertificateStatus.Revoked;
                status.RevocationTime = revokedStatus.RevocationTime;
                try
                {
                    status.RevocationReason = revokedStatus.RevocationReason;
                } catch (InvalidOperationException)
                {
                    status.RevocationReason = -1;
                }
            }
            else
                status.Status = OCSPCertificateStatus.Unknown;

            return status;
        }
    }
}
