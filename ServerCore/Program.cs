using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerCore
{
    internal class Program
    {
        static Listener _listener = new Listener();
        static void OnAcceptHandler(Socket clientSocket)
        {
            try
            {   
                Session session = new Session();
                session.Start(clientSocket);

                byte[] sendbuffer = Encoding.UTF8.GetBytes("Welcome to Server!");
                session.Send(sendbuffer);
                
                Thread.Sleep(1000);

                session.Disconnect();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
        static void Main(string[] args)
        {
            // DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

           
            _listener.Init(endPoint, OnAcceptHandler);
            Console.WriteLine("Listening....");

            while (true)
            {              
                                                   
            }
           
            
        }
    }
}
