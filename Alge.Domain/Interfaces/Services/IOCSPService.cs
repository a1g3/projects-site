using Alge.Domain.Dtos;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;

namespace Alge.Interfaces.Services
{
    public interface IOCSPService
    {
        OcspReq CreateOCSPReq(X509Certificate certificate, X509Certificate issuer);
        IList<Uri> GetOCSPUris();
        BasicOcspResp GetOCSPStatus(OcspReq OCSPrequest);
        OCSPDto ParseOCSPResponse(BasicOcspResp brep);
    }
}
