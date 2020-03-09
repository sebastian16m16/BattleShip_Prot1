using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace BattleShips.PreStartForms
{
    public partial class TileInfo : Form
    {
        SimpleTcpClient client;
        string player;
        public TileInfo(SimpleTcpClient theClient, string playerName)
        {
            this.client = theClient;
            this.player = playerName;
            InitializeComponent();
            this.Width = 1087;
            this.Height = 700;
        }

        private void TileInfo_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            pictureBox1.Image = Properties.Resources.TileInfo;
            this.okButton.Location = new Point(this.Width / 2 - this.okButton.Width / 2, this.Height - 70);
            this.pictureBox1.Width = 1047;
            this.pictureBox1.Height = 591;
            this.pictureBox1.Location = new Point(10,  10);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form form = new Form1(client, player);
            form.Show();
        }
    }
}
