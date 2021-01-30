using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

// References:
// https://www.c-sharpcorner.com/article/sending-an-hl7-message-receiving-it-using-a-listener-and-sending-an-acknowledge/
// https://healthcareitsystems.com/2012/05/20/sample-hl7-adt-a04-message/

namespace HL7Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipstr = "127.0.0.1";
            int port = 8888;
            string filePath = @"sample.hl7";

            if (args.Length > 0){
                filePath = args[0];
                ipstr = args[1];
                port = int.Parse(args[2]);
            }

            Console.WriteLine("HL7 Sender - Usage: >dotnet HL7Sender.dll [FILE PATH] [IP ADDRESS] [PORT]");
            Console.WriteLine($"Sending - {filePath} to {ipstr}:{port}");
            
            // Build Payload from local file
            byte[] contents = File.ReadAllBytes(filePath);

            // header 1 byte
            byte VT = 0x0b;

            // terminator 2 bytes
            byte FS = 0x1c;
            byte CR = 0x0d;

            byte[] payload = new byte[contents.Length + 3]; // additional 3 bytes with header + terminator

            payload[0] = VT;
            Array.Copy(contents, 0, payload, 1, contents.Length); // copy N-bytes of actual data
            payload[contents.Length+1] = FS;
            payload[contents.Length+2] = CR;
            
            // Send it to Mirth Server
            IPAddress ipaddr = IPAddress.Parse(ipstr);
            IPEndPoint ipend = new IPEndPoint(ipaddr, port);
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            sender.Connect(ipend);
            sender.SendBufferSize = 4096;
            sender.Send(payload);

            sender.Close();

            Console.WriteLine("Done.");
        }
    }
}
