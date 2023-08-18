using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class ServerCommunicator
    {
        public string message { get; set; }
        public ServerCommunicator() {
            message = string.Empty;
        }

        public void ServerSender(Socket sc) 
        {
            if (message != string.Empty)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                sc.Send(buffer);
               
            }
        }

        public string ServerRecive(Socket sc)
        {
            if (sc.Poll(1000, SelectMode.SelectRead))
            {
                byte[] buffer = new byte[1024];
                int received_bytes_count = sc.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, received_bytes_count);
                return message;
            }
            return message;
        }
    }
}
