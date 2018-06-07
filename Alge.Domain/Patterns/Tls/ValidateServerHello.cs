using System.Collections.Generic;
using Alge.Domain.Exceptions;

namespace Alge.Domain.Patterns.Tls
{
    public class ValidateServerHello : TlsValidatorBase
    {
        public override void HandleValidation(List<byte> serverResponse)
        {
            if (serverResponse[5] != 0x02)
                throw new InvalidServerHelloException("The server did not send a valid ServerHello!");

            Successor.HandleValidation(serverResponse);
        }
    }
}
