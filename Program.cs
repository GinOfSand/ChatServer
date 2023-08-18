using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    internal class Program
    {


        static void Main(string[] args)
        {
            try
            {
                ServerService ss = new ServerService("192.168.31.12", 1024);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            
        }
    }
}
