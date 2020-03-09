using System;
using System.Drawing;
using System.Windows.Forms;

namespace BattleShips.Resources.Models.Items
{
    class CruiserBoat : Tile
    {

        public CruiserBoat(int x, int y)
        {
            this.Name = $"Cruiser_Boat_{x}_{y}";
            this.Location = new Point(x * length, y * length);
            this.GPosition = new Point(x, y);
            this.Size = new Size(length, length);
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.SQsize = 3;
            this.ships = 2;
            this.Image = Properties.Resources.CruiserBoat;
            this.type = typeof(CruiserBoat);
            this.tileName = "Cruiser";

        }
    }
}
