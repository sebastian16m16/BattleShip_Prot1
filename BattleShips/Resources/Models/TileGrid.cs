using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace BattleShips.Resources.Models
{
    class TileGrid : Panel
    {
        public Size gridSize { get; set; }
        public Tile[,] tiles { get; set; }
        public Image tilesImage { get; set; }

        public void LoadGrid(Size gridSize, Image tileImage)
        {
            this.Controls.Clear();

            this.gridSize = gridSize;
            this.Size = new Size(gridSize.Width * Tile.length, gridSize.Height * Tile.length);
            this.tiles = new Tile[gridSize.Width, gridSize.Height];
            this.tilesImage = tileImage;

            for(int x = 0; x < gridSize.Width; x++)
            {
                for(int y = 0; y < gridSize.Height; y++)
                {
                    Tile tile = new Tile(x, y, tileImage, this.Location);
                    tiles[x,y] = tile;
                   
                    this.Controls.Add(tile); 
                }
            }
        }

        public void inheritGrid(TileGrid other)
        {
            for(int i=0; i< gridSize.Width; i++)
            {
                for(int j=0; j < gridSize.Height; j++)
                {
                   // Console.WriteLine(" BEFORE: This.name[" + i + "," + j + "] = " + this.tiles[i, j].tileName + ",   Other.name[" + i + "," + j + "] = " + other.tiles[i, j].tileName);
                    this.tiles[i, j].inheritTileInfo(other.tiles[i, j]);
                    //Console.WriteLine(" AFTER: This.name[" + i + "," + j + "] = " + this.tiles[i, j].tileName + ",   Other.name[" + i + "," + j + "] = " + other.tiles[i, j].tileName);
                }
            }
        }

       

       
    }
}
