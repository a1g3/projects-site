using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Alge.Domain.Enums;
using Alge.Domain.Interfaces.Strategy;
using Alge.Interfaces.Services;

namespace Alge.Domain.Services
{
    public abstract class TlsSelectionService : Strategy<Tls>, ITlsScanService
    {
        public abstract List<CipherSuites> Scan(string hostname);

        public List<byte> Connect(string hostname, byte[] clientHello)
        {
            List<byte> serverHello = new List<byte>();
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect(hostname, 443);
                
                using(var networkStream = tcpClient.GetStream())
                {
                    byte[] readBuffer = new byte[1];
                    tcpClient.ReceiveTimeout = 3000;
                    tcpClient.SendTimeout = 3000;
                    networkStream.ReadTimeout = 3000;
                    networkStream.WriteTimeout = 3000;

                    networkStream.Write(clientHello, 0, clientHello.Length);

                    try
                    {
                        while (networkStream.DataAvailable)
                        {
                            networkStream.Read(readBuffer, 0, readBuffer.Length);
                            foreach (byte b in readBuffer)
                                serverHello.Add(b);
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }

            return serverHello;
        }

        public List<byte> CreateBasicClientHandshake(Tls protocolVersion, CipherSuites cipher)
        {
            byte[] clientRandom = new byte[28];
            Random RNG = new Random();
            RNG.NextBytes(clientRandom);

            UInt32 timeStamp = (UInt32)(DateTime.UtcNow - (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))).TotalSeconds;  // Total seconds elapsed in epoch.
            byte[] readBuffer = new byte[1];
            byte[] timeStampBytes = BitConverter.GetBytes(timeStamp);
            byte[] version = BitConverter.GetBytes((ushort)protocolVersion);
            byte[] cipherSuite = BitConverter.GetBytes((ushort)cipher).Reverse().ToArray();
            Array.Reverse(timeStampBytes);
            Array.Reverse(version);

            var clientHello = new List<byte>();
            clientHello.Add(0x16);                                                         // Handshake        [Record Layer][0]
            clientHello.AddRange(version);                                                 // Version Number   [Record Layer][1,2]
            clientHello.AddRange(new byte[] { 0x00, 0x00 });                               // Message Length   [Record Layer][3,4]
            clientHello.Add(0x01);                                                         // Client Hello     [Begin Handshake][5]
            clientHello.Add(0x00);                                                         // Message Length   [6]
            clientHello.Add(0x00);                                                         // Message Length   [7]
            clientHello.Add(0x00);                                                         // Message Length   [8]
            clientHello.AddRange(version);                                                 // Version Number   [9,10]
            clientHello.AddRange(timeStampBytes);                                          // Unix Timestamp   [11-14]
            clientHello.AddRange(clientRandom);                                            // 28 Random Bytes  [15-43]
            clientHello.Add(0x00);                                                         // SessionID Length [44]
            clientHello.Add(0x00);                                                         // Cipher Suite Length (2 bytes)
            clientHello.Add(0x02);                                                         //
            clientHello.AddRange(cipherSuite);                                             // Add 1 cipher to the list (2 bytes.) If the server responds with a ServerHello, it supports that cipher.
            clientHello.Add(0x02);                                                         // Compression List Length
            clientHello.Add(0x01);                                                         // Deflate
            clientHello.Add(0x00);                                                         // Null

            return clientHello;
        }
    }

    public class Ssl2ScanService : TlsSelectionService
    {
        public override Tls StrategyType => Tls.SSL2;

        public override List<CipherSuites> Scan(string hostname)
        {
            throw new NotImplementedException();
        }
    }

    public class TlsScanService : TlsSelectionService
    {
        public override Tls StrategyType => Tls.TLS12;

        public override List<CipherSuites> Scan(string hostname)
        {
            //Get ciphers for TLS1.2
            var clientHello = CreateBasicClientHandshake(Tls.TLS12, CipherSuites.NULL).ToArray();

            var response = Connect(hostname, clientHello);

            throw new NotImplementedException();
        }
    }
}
