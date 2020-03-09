using System;
using System.Drawing;
using System.Windows.Forms;



namespace BattleShips.Resources.Models.Items
{
    class PatrolBoat : Tile
    {
        public PatrolBoat() { }
        public PatrolBoat(int x, int y)
        {
            this.Name = $"Patrol_Boat_{x}_{y}";
            this.Location = new Point(x * length, y * length);
            this.GPosition = new Point(x, y);
            this.Size = new Size(length, length);
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.SQsize = 2;
            this.ships = 2;
            this.Image = Properties.Resources.PatrolBoat;
            this.type = typeof(PatrolBoat);
            this.tileName = "Patrol Boat";


        }
    }
}
