using System.Collections.Generic;
using Alge.Domain.Exceptions;

namespace Alge.Domain.Patterns.Tls
{
    public class ValidateServerHandshake : TlsValidatorBase
    {
        public override void HandleValidation(List<byte> serverResponse)
        {
            if (serverResponse[0] != 0x16)
                throw new InvalidServerHelloException("The server did not send a valid handshake message!");

            Successor.HandleValidation(serverResponse);
        }
    }
}
