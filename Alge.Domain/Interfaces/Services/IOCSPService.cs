using Alge.Domain.Dtos;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;

namespace Alge.Interfaces.Services
{
    public interface IOcspService
    {
        OcspReq CreateOcspReq(X509Certificate certificate, X509Certificate issuer);
        IList<Uri> GetOcspUris();
        BasicOcspResp GetOcspStatus(OcspReq ocspRequest);
        OcspDto ParseOcspResponse(BasicOcspResp brep);
    }
}
