using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Net;
using System.Net.Sockets;

namespace Vault13Server
{
    public class Programm
    {
        static VaultServer VaultServer;

        static void Main(string[] args)
        {
            VaultTecServerProtocol vaultTecServerProtocol = new VaultTecServerProtocol();
            VaultServer = new VaultServer();

                       
            Console.WriteLine("Robco industries unified operating system");
            Console.WriteLine("COPYRIGHT 2075-2077 ROBCO INDUSTRIES");
            Console.WriteLine("Server 1");

            while (true)
            {
                string reply = VaultServer.ProcessClientRequestIfNeed();
                if (reply != null)
                    Console.WriteLine("<< " + reply);
            }        
        }
    }
}
