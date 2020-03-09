using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTCP;

namespace BattleShips.Resources.Server
{
    class BSServer : SimpleTcpServer
    {
        int maxClients;
        int clientsConnected;
        string clientMessage;
        string HOST_ip;
        string HOST_port;

        //
        //Constructor (Initialize)
        //
        public BSServer()
        {
            this.StringEncoder = Encoding.UTF8;
            maxClients = 2;
            this.DataReceived += data_received;
            this.HOST_ip = "10.129.62.128";
            this.HOST_port = "7000";
           
        }

        //
        //React to client input
        //
        private void data_received(object sender, Message e)
        {
            clientMessage = e.MessageString;
            Console.WriteLine(clientMessage);
            if (clientMessage.Equals("OK"))
            {
                
            }

        }

        //
        //Second player is disconnected
        //
        public void sendNoSecondPlayerERROR()
        {
            this.BroadcastLine("Second Player Disconnected!");
        }

        //
        //Start the server with the embedded info
        //
        public void startBSServer()
        {
            System.Net.IPAddress ip = new System.Net.IPAddress(long.Parse(HOST_ip));
            this.Start(ip, Convert.ToInt32(HOST_port));
        }


        //
        //stop the server
        //
        public void stopBSServer()
        {
            if (this.IsStarted)
            {
                this.Stop();
            }
        }

        
    }
}
