using System.Collections.Generic;
using Alge.Domain.Exceptions;

namespace Alge.Domain.Patterns.Tls
{
    public class ValidateServerHelloSize : TlsValidatorBase
    {
        public override void HandleValidation(List<byte> serverResponse)
        {
            if (serverResponse.Count < 64)
                throw new InvalidServerHelloException();

            Successor.HandleValidation(serverResponse);
        }
    }
}
