using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Vault13ServerTests
{

    class SimpleClient2Tests
    {
        IPAddress _serverIp = new IPAddress(new byte[] { 127, 0, 0, 1 });
        int _serverPort = 8000;
        Socket _connectionSocket = new Socket(AddressFamily.InterNetwork,
                                               SocketType.Stream,
                                               ProtocolType.Tcp);

        IPEndPoint _serverIpEndPoint;

        public SimpleClient2Tests(int port = 8000)
        {
            _serverIpEndPoint = new IPEndPoint(_serverIp, port);
        }

        public string SendRequestAndGetResponse(string request)
        {
            string response;
            byte[] messageToServer = Encoding.UTF8.GetBytes(request);
            byte[] rxBuf = new byte[1024];

            _connectionSocket.Connect(_serverIpEndPoint);

            _connectionSocket.Send(messageToServer);
            _connectionSocket.Receive(rxBuf);

            response = Encoding.UTF8.GetString(rxBuf, 0, rxBuf.Count());
            response = response.Substring(0, response.IndexOf('\0'));//удаляем все после первого вхождения \0

            return response;
        }
    }
}
