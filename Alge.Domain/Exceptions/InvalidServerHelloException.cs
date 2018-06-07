using System;

namespace Alge.Domain.Exceptions
{
    [Serializable]
    public class InvalidServerHelloException : Exception
    {
        public InvalidServerHelloException() { }
        public InvalidServerHelloException(string message) : base(message) { }
        public InvalidServerHelloException(string message, Exception inner) : base(message, inner) { }
        protected InvalidServerHelloException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
