using Alge.Domain.Dtos;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;

namespace Alge.Interfaces.Services
{
    public interface IOcspService
    {
        OcspReq CreateOCSPReq(X509Certificate certificate, X509Certificate issuer);
        IList<Uri> GetOCSPUris();
        BasicOcspResp GetOCSPStatus(OcspReq OCSPrequest);
        OcspDto ParseOCSPResponse(BasicOcspResp brep);
    }
}
