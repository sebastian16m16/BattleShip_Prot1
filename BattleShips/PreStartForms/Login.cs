using SimpleTCP;
using System.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShips.PreStartForms
{
    public partial class Login : Form
    {

        SimpleTcpClient client;
        TileInfo tileInfo ;


        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.Delimiter = 0x0;
            client.DataReceived += data_received;
            this.CenterToScreen();
            this.playerNameBox.Text = "Sebastian";
            this.serverAddressBox.Text = "10.129.62.128";
            this.portBox.Text = "7000";
            tileInfo = new TileInfo(client, playerNameBox.Text);
            conectingLabel.Hide();
        }

        private void data_received(object sender, SimpleTCP.Message e)
        {
            //
            //get text from server
            //
            string text = "";
            text = e.MessageString;
            Console.WriteLine(text);

            if(text == "SEND_NAME")
            {
                client.WriteLineAndGetReply(this.playerNameBox.Text, TimeSpan.FromSeconds(2));

                Console.WriteLine(this.playerNameBox.Text);

            }else if(text == "SUCCESS")
            {
                client.WriteLineAndGetReply("OK", TimeSpan.FromSeconds(2));
                Console.WriteLine("OK replied --- to SUCCESS");

            }else if(text == "FAILED")
            {
                //
                //SHOW message error
                //
                MessageBox.Show("Connection Failed!! Something went wrong! \n\t\tTRY AGAIN!");
            }
            else
            {
                client.WriteLineAndGetReply("OK", TimeSpan.FromSeconds(2));
                Console.WriteLine(string.Format("Other player: {0}", text));

                hideTHIS();
                TileInfo tileInfo = new TileInfo(client, this.playerNameBox.Text);
                tileInfo.Show();
            }


        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            
            client.Connect(serverAddressBox.Text, Convert.ToInt32(portBox.Text));
            conectingLabel.Show();
            //
            //send Player name and ready for connection status to server
            //
            //client.WriteLineAndGetReply(playerNameBox.Text, TimeSpan.FromSeconds(2));
            //client.WriteLineAndGetReply("Ready", TimeSpan.FromSeconds(2));
            
        }


        //
        // GET LOCAL IP ADDRESS
        //
        [Obsolete]
        public static string GetLocalIPAddress()
        {
            string myIP = "";
            try
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                Console.WriteLine(hostName);
                // Get the IP  
                myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                Console.WriteLine("My IP Address is :" + myIP);

            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }

            return myIP;

        }

        //
        //Blank delegate
        //
        private delegate void BlankDelegate();

        //
        //INVOKE Parent
        //
        public void hideTHIS()
        {
            this.Invoke((MethodInvoker)delegate
            {
                // close the form on the forms thread
                this.Hide();
                this.tileInfo.Show();
            });
        }
    }
}
