using System;
using System.Net.Sockets;

namespace ReportsClient
{
    public class ServerStatus
    {
        public string ServerAdress { get; set; }
        public bool ServerState { get; set; }

        public ServerStatus()
        {

        }

        private bool GetServerState()
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(ServerAdress, 80);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
 