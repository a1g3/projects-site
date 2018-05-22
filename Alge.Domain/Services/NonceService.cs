using Alge.Interfaces.Services;
using System;
using System.Security.Cryptography;

namespace Alge.Domain.Services
{
    public class NonceService : INonceService
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
        private readonly string Nonce;

        public NonceService()
        {
            byte[] nonceBytes = new byte[66];
            rng.GetBytes(nonceBytes);

            Nonce = Convert.ToBase64String(nonceBytes);
        }

        public string GetNonce()
        {
            return Nonce;
        }
    }
}
