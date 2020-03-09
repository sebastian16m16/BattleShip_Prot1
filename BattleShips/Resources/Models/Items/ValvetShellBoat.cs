using System;
using System.Drawing;
using System.Windows.Forms;

namespace BattleShips.Resources.Models.Items
{
    class ValvetShellBoat : Tile
    {
        public ValvetShellBoat(int x, int y)
        {
            this.Name = $"ValvetShell_Boat_{x}_{y}";
            this.Location = new Point(x * length, y * length);
            this.GPosition = new Point(x, y);
            this.Size = new Size(length, length);
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.SQsize = 4;
            this.ships = 1;
            this.Image = Properties.Resources.ValvetShellBoat;
            this.type = typeof(ValvetShellBoat);
            this.tileName = "ValvetShell";

        }
    }
}
