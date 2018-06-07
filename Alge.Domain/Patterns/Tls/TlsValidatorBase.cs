using System.Collections.Generic;

namespace Alge.Domain.Patterns.Tls
{
    public abstract class TlsValidatorBase
    {
        protected TlsValidatorBase Successor { get; set; }

        public abstract void HandleValidation(List<byte> serverResponse);

        public void SetSuccessor(TlsValidatorBase successor)
        {
            this.Successor = successor;
        }
    }
}
