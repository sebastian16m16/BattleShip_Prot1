using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using BattleShips.Properties;




namespace BattleShips.Resources
{
    class Tile : PictureBox
    {
        public TileState state { get; set; }
        public Point GPosition { get; set; }
        internal const int length = 50;

        public int x, y;
        public Point GridLocation { get; set; }
        public Point absolutLocation { get; set; }
        public Color tileColor { get; set; }
        public int SQsize { get; set; }
        public int ships { get; set; }
        public Type type { get; set; }

        private Image blank = Properties.Resources.blankTile;
        public string tileName { get; set; }


        public Tile() { }

        public Tile(int x , int y, Image tileImage, Point loc)
        {
            this.x = x;
            this.y = y;
            this.Name = $"Tile_{x}_{y}";
            this.Location = new Point(x * length, y * length);
            this.GridLocation = loc;
            this.absolutLocation = new Point(loc.X + length * x, loc.Y + length);
            this.GPosition = new Point(x, y);
            this.Size = new Size(length, length);
            this.Image = tileImage;
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.state = TileState.UNTOUCHED;
            this.SQsize = 0;
            this.type = typeof(Tile);
            this.tileName = "freeTile";
            
        }

        public void setState(TileState newState)
        {
            this.state = newState;

            switch (newState)
            {
                case TileState.HIT: this.Image = Properties.Resources.hit; break;
                case TileState.MISS: this.Image = Properties.Resources.missTile; break;
            }

        }

      public void inheritTileInfo(Tile o)
        {
            this.state = o.state;
            this.Image = o.Image;
            this.Name = o.Name;
            this.SQsize = o.SQsize;
            this.ships = o.ships;
            this.GPosition = o.GPosition;
            this.type = o.type;
            this.tileName = o.tileName;
           
        }

        

        public void updateGridLocation(Point newLocation)
        {
            GridLocation = newLocation;
            absolutLocation = new Point(GridLocation.X + length * x, GridLocation.Y + y * length);
        }

        public Point getNumberLabelLocation()
        {
            return new Point(this.absolutLocation.X + 3, this.absolutLocation.Y + 3);
        }

        public Point getSizeLabelLocation()
        {
            return new Point(this.absolutLocation.X + length - 15, this.absolutLocation.Y + length - 15);
        }

        public Point getNameLabelLocation()
        {
            return new Point(this.absolutLocation.X + length , this.absolutLocation.Y + length/2);
        }

        public Color getTileColor()
        {
            Bitmap img = new Bitmap(this.Image);

           return img.GetPixel(img.Width / 2, img.Height / 2);
        }

        public override bool Equals(object obj)
        {
            return obj is Tile tile &&
                   state == tile.state &&
                   EqualityComparer<Point>.Default.Equals(GPosition, tile.GPosition) &&
                   EqualityComparer<Color>.Default.Equals(tileColor, tile.tileColor) &&
                   SQsize == tile.SQsize &&
                   ships == tile.ships;
        }

        public Type getType()
        {
            return this.type;
        }

        public void clear()
        {
            this.Image = blank;
            this.state = TileState.UNTOUCHED;
            this.type = typeof(Tile);
        }


        


    }
}
