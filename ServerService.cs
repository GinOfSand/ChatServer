using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ChatServer
{
    public class ServerService : IDisposable
    {
        public Socket server { get; set; }
        public List<Socket> client { get; set; }
        public ServerCommunicator Communication { get; set; }
      

        public ServerService(string ip, int port)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client = new List<Socket>();
            Communication = new ServerCommunicator();
            IPAddress ServerIP = IPAddress.Parse(ip);
            IPEndPoint ServerEP = new IPEndPoint(ServerIP, port);
            server.Bind(ServerEP);
            server.Listen(100);
            Thread th = new Thread(new ThreadStart(() =>
            {
                serverwaiter();
            }));
            Thread thread = new Thread(new ThreadStart(() =>
            {
                ReciveSender();
            }));
            //Thread dis = new Thread(new ThreadStart(() =>
            //{
            //    ClientDisconnected();
            //}));

            th.Start();
            thread.Start();
            //dis.Start();




        }
        //public void ClientDisconnected()
        //{

        //    while (client != null)
        //    {
        //        for (int y = 0; y < client.Count; y++)
        //        {
        //            if (client[y].Poll(500,SelectMode.SelectError))
        //            {
        //                lock (client)
        //                {
        //                    Console.WriteLine("Клиент отключился");

        //                    client[y].Close();
        //                    client.RemoveAt(y);
        //                }
        //            }
        //        }
        //    }
        //}

        public void serverwaiter()
        {
           
                while (true)
                {
                    Console.WriteLine("Ожидание клиентов!");
                     
                    Socket cl = server.AcceptAsync().Result;

                    lock (client)
                    {
                    client.Add(cl);
                    Console.WriteLine("Клиент " + client.LastOrDefault().RemoteEndPoint + " подключен.");
                    }
                    Thread.Sleep(100);
                }
            
                
        }


        public void ReciveSender()
        {
            while (true)
            {
                for (int y = 0; y < client.Count; y++)
                {
                    
                        Communication.ServerRecive(client[y]);

                        for (int i = 0; i < client.Count; i++)
                            if (client[i] != client[y])
                            {
                                Communication.ServerSender(client[i]);
                            }
                        Communication.message = string.Empty;
                    
                }
                
            }
            
        }

        public void Dispose()
        {
            server.Close();
            foreach (Socket s in client)
            {
                s.Close();
            }
        }
    }
}
