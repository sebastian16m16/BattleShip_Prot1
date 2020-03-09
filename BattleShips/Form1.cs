using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleShips.Resources.Models.Items;
using BattleShips.Resources.Utilities;
using BattleShips.Resources;
using SimpleTCP;
using System.Text.RegularExpressions;

namespace BattleShips
{
    public partial class Form1 : Form
    {
        SimpleTcpClient client;
        string player;
        public Form1(SimpleTcpClient theClient, string playerName)
        {
            this.client = theClient;
            this.player = playerName;
            InitializeComponent();
        }

        

        //
        //Declare Local Variables
        //
        Stack<EventState> eventStates = new Stack<EventState>();
        Tile selected = new Tile();
        Tile prev = new Tile();
        byte clicks = 0;
        int rows = 10;
        int columns = 10;
        Match match;
        int pX;
        int pY;
        bool gameOn = false;
        bool connected = false;

        //
        // First Load of WINDOW
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            Font font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold );
            this.YourGrid.Text = player;
            
            //personalGrid
            this.personalGrid.LoadGrid(new Size(rows, columns), Properties.Resources.blankTile);
            foreach (BattleShips.Resources.Tile tile in this.personalGrid.Controls)
            {
                tile.MouseDown += personalGrid_MouseDown;
            }

            //battleShipsGRID
            this.battleShipsGRID.LoadGrid(new Size(1, 5), Properties.Resources.blankTile);
            this.battleShipsGRID.Location = new Point(this.personalGrid.Location.X + this.personalGrid.Width + 20, this.personalGrid.Location.Y + this.personalGrid.Height/4);

            
            //customize battleShopGRID tiles
            foreach(BattleShips.Resources.Tile tile in this.battleShipsGRID.Controls){
                if(tile.GPosition == new Point(0, 0)) {

                    //Tile image
                    tile.inheritTileInfo(new PatrolBoat(tile.x, tile.y));
                    tile.updateGridLocation(this.battleShipsGRID.Location);
                    tile.MouseDown += select_ship;

                    //assign Labels
                    this.patrolNumberLabel.Text = tile.ships.ToString();
                    this.patrolSizeLabel.Text = tile.SQsize.ToString();
                    
                    //setBackColor to label
                    this.patrolNumberLabel.BackColor = tile.getTileColor();
                    this.patrolSizeLabel.BackColor = tile.getTileColor();

                    //name and place label
                    this.patrolLabel.Text = "Patrol";
                    this.patrolNumberLabel.Location = tile.getNumberLabelLocation();
                    this.patrolSizeLabel.Location = tile.getSizeLabelLocation();
                    this.patrolLabel.Location = tile.getNameLabelLocation();
                }

                if(tile.GPosition == new Point(0, 1)) {
                    //Tile image
                    tile.inheritTileInfo(new CruiserBoat(tile.x, tile.y));
                    tile.updateGridLocation(this.battleShipsGRID.Location);
                    tile.MouseDown += select_ship;

                    //assign Labels
                    this.cruiserNumberLabel.Text = tile.ships.ToString();
                    this.cruiserSizeLabel.Text = tile.SQsize.ToString();

                    //setBackColor to label
                    this.cruiserNumberLabel.BackColor = tile.getTileColor();
                    this.cruiserSizeLabel.BackColor = tile.getTileColor();

                    //name and place label
                    this.cruiserLabel.Text = "Cruiser";
                    this.cruiserNumberLabel.Location = tile.getNumberLabelLocation();
                    this.cruiserSizeLabel.Location = tile.getSizeLabelLocation();
                    this.cruiserLabel.Location = tile.getNameLabelLocation();
                }

                if(tile.GPosition == new Point(0, 2)) {
                    //Tile image
                    tile.inheritTileInfo(new ValvetShellBoat(tile.x, tile.y));
                    tile.updateGridLocation(this.battleShipsGRID.Location);
                    tile.MouseDown += select_ship;

                    //assign Labels
                    this.valvetNumberLabel.Text = tile.ships.ToString();
                    this.valvetSizeLabel.Text = tile.SQsize.ToString();

                    //setBackColor to label
                    this.valvetNumberLabel.BackColor = tile.getTileColor();
                    this.valvetSizeLabel.BackColor = tile.getTileColor();

                    //name and place label
                    this.valvetLabel.Text = "ValvetShell";
                    this.valvetNumberLabel.Location = tile.getNumberLabelLocation();
                    this.valvetSizeLabel.Location = tile.getSizeLabelLocation();
                    this.valvetLabel.Location = tile.getNameLabelLocation();
                }

                if(tile.GPosition == new Point(0, 3)) {
                    //Tile image
                    tile.inheritTileInfo(new RedCrowBoat(tile.x, tile.y));
                    tile.updateGridLocation(this.battleShipsGRID.Location);
                    tile.MouseDown += select_ship;

                    //assign Labels
                    this.redNumberLabel.Text = tile.ships.ToString();
                    this.redSizeLabel.Text = tile.SQsize.ToString();

                    //setBackColor to label
                    this.redNumberLabel.BackColor = tile.getTileColor();
                    this.redSizeLabel.BackColor = tile.getTileColor();

                    //name and place label
                    this.redLabel.Text = "RedCrow";
                    this.redNumberLabel.Location = tile.getNumberLabelLocation();
                    this.redSizeLabel.Location = tile.getSizeLabelLocation();
                    this.redLabel.Location = tile.getNameLabelLocation();
                }
                if(tile.GPosition == new Point(0, 4)) {
                    //Tile image
                    tile.inheritTileInfo(new VDragonBoat(tile.x, tile.y));
                    tile.updateGridLocation(this.battleShipsGRID.Location);
                    tile.MouseDown += select_ship;

                    //assign Labels
                    this.dragonNumberLabel.Text = tile.ships.ToString();
                    this.dragonSizeLabel.Text = tile.SQsize.ToString();

                    //setBackColor to label
                    this.dragonNumberLabel.BackColor = tile.getTileColor();
                    this.dragonSizeLabel.BackColor = tile.getTileColor();

                    //name and place label
                    this.dragonLabel.Text = "VDragon";
                    this.dragonNumberLabel.Location = tile.getNumberLabelLocation();
                    this.dragonSizeLabel.Location = tile.getSizeLabelLocation();
                    this.dragonLabel.Location = tile.getNameLabelLocation();

                    //change color of text
                    this.dragonNumberLabel.ForeColor = Color.White;
                    this.dragonSizeLabel.ForeColor = Color.White;

                    //connected indicator location
                    this.connectedPictureBox.Location = new Point(this.Width + 150, this.Height);
                    this.connectedPictureBox.Size = Properties.Resources.connected.Size;
                    this.connectedPictureBox.Image = Properties.Resources.connected;

                    this.connectedLabel.Text = "Connected";
                    this.connectedLabel.Location = new Point(this.connectedPictureBox.Location.X + Properties.Resources.connected.Size.Width, this.connectedPictureBox.Location.Y + 20);
                }
            }
           
            

            //opponentGrid
            this.opponentGrid.LoadGrid(new Size(rows, columns), Properties.Resources.opponentTile);
            this.MaximumSize = this.MinimumSize = new Size(this.opponentGrid.Width + this.personalGrid.Width + 200, this.opponentGrid.Height + 150);
            this.opponentGrid.Location = new Point(this.MaximumSize.Width - this.opponentGrid.Width - BattleShips.Resources.Tile.length, this.personalGrid.Location.Y);
            foreach(BattleShips.Resources.Tile tile in this.opponentGrid.Controls)
            {
                tile.MouseDown += opponentGrid_MouseDown;
            }
           
            //playerLabels
            this.YourGrid.Location = new Point(this.personalGrid.Location.X + this.personalGrid.Width / 2, 10);
            this.EnemyGrid.Location = new Point(this.opponentGrid.Location.X + this.personalGrid.Width / 2, 10);
            this.YourGrid.Font = this.EnemyGrid.Font = font;

            //System Messages Area
            this.sysMsgs.Text = "You must place your BattleShips strategically, and after you did so, you can start the game! \n You must use every ship!!!";
            this.sysMsgs.Font = font;
            this.sysMsgs.Location = new Point(this.personalGrid.Location.X + this.personalGrid.Width/2, this.personalGrid.Height + 70);

            //Start game button location
            this.startGameBTN.Location = new Point(this.battleShipsGRID.Location.X + this.battleShipsGRID.Width/2 - 20, 30);
            this.startGameBTN.Width = 100;

            // CTRL + Z
            this.KeyDown += new KeyEventHandler(ctrl_Z);

            //save state of game
            Console.WriteLine(" ----- EVENT PUSHED -----");
            this.eventStates.Push(new EventState(this.personalGrid, this.battleShipsGRID, clicks));

            //get server data
            client.DataReceived += data_received;


            
        }

        private void data_received(object sender, SimpleTCP.Message e)
        {
            string text = "";

            text = e.MessageString;

            match = Regex.Match(text, @"\d \d");

            if(match.Success)
            {
                match = Regex.Match(text, @"\d");
                if (match.Success)
                {
                    pX = Int32.Parse(match.Value);
                }
                match = match.NextMatch();

                if (match.Success)
                {
                    pY = Int32.Parse(match.Value);
                }

                if(this.personalGrid.tiles[pX, pY].state == TileState.OCCUPIED)
                {
                    this.personalGrid.tiles[pX, pY].state = TileState.HIT;
                }
                else
                {
                    this.personalGrid.tiles[pX, pY].state = TileState.MISS;
                }
                
            }

            //change connection status
            if(text == "DISCONNECTED")
            {
                this.connectedPictureBox.Image = Properties.Resources.disconnected;
                this.connectedLabel.Text = "Disconnected";
            }
        }

        //    
        // CTRL + Z 
        //
        private void ctrl_Z(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Z && (e.Control))
            {
                EventState previousState = eventStates.Pop();
                this.personalGrid.inheritGrid( previousState.getLastPersonalGrid());
                this.battleShipsGRID.inheritGrid(previousState.getLastBattleshipGrid());
                this.clicks = previousState.getClicks();

                for(int i=0; i<5; i++)
                {
                    this.battleShipsGRID.tiles[0, i].ships = previousState.getLastBattleshipGrid().tiles[0, i].ships;

                    if (i == 0) this.patrolNumberLabel.Text = previousState.getLastBattleshipGrid().tiles[0, i].ships.ToString();
                    if (i == 1) this.cruiserNumberLabel.Text = previousState.getLastBattleshipGrid().tiles[0, i].ships.ToString();
                    if (i == 2) this.valvetNumberLabel.Text = previousState.getLastBattleshipGrid().tiles[0, i].ships.ToString();
                    if (i == 3) this.redNumberLabel.Text = previousState.getLastBattleshipGrid().tiles[0, i].ships.ToString();
                    if (i == 4) this.dragonNumberLabel.Text = previousState.getLastBattleshipGrid().tiles[0, i].ships.ToString();
                }



                Console.WriteLine("CTRL + Z");
            }
        }
        //
        // OpponentGrid interaction
        //
        private void opponentGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if(((Tile)sender).state == TileState.OCCUPIED)
            {
                ((Tile)sender).state = TileState.HIT;
                ((Tile)sender).Image = Properties.Resources.hit;
            }

            if(((Tile)sender).state == TileState.MISS || ((Tile)sender).state == TileState.HIT)
            {
                MessageBox.Show("Only a fool shoots twice in the same place where he missed and expecting to hit the second time!!!");
            }

            if (((Tile)sender).state == TileState.UNTOUCHED)
            {
                ((Tile)sender).state = TileState.MISS;
                ((Tile)sender).Image = Properties.Resources.missTile;
            }
        }

        //
        // PersonalGrid interaction
        //
        private void personalGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if(this.battleShipsGRID.tiles[selected.x, selected.y].ships == 0)
            {
                selected.type = null;
            }

            if (selected.getType() != null)
            {
                Console.WriteLine(" ----- EVENT PUSHED -----");

                this.eventStates.Push(new EventState(this.personalGrid, this.battleShipsGRID, clicks));
            }

            if (clicks == 0)
            {
                if (selected.getType() == null)
                {
                    clicks = 0;
                    return;
                }

                if (this.battleShipsGRID.tiles[selected.x, selected.y].ships > 0)
                {

                    if (((Tile)sender).state == TileState.UNTOUCHED)
                    {
                        clicks++;
                        ((Tile)sender).inheritTileInfo(selected); // place boat
                        prev = ((Tile)sender);
                              
                    }
                    else
                    {
                        //dialog box
                        MessageBox.Show("You can't place boats on top of eachother...");
                    }
                }
                        
               
            }
            else if(clicks == 1)
            {

                if(((Tile)sender).x - prev.x < ((Tile)sender).y - prev.y && (((Tile)sender).y - prev.y) > 0)
                {
                    addTilesOnYaxis();

                }
                else if(((Tile)sender).x - prev.x > ((Tile)sender).y - prev.y && (((Tile)sender).x - prev.x) > 0)
                {
                    addTilesOnXaxis();

                }else if(((Tile)sender).x - prev.x == ((Tile)sender).y - prev.y && (((Tile)sender).x - prev.x) > 0)
                {
                    if(columns - prev.x >= this.battleShipsGRID.tiles[selected.x, selected.y].ships)
                    {
                        addTilesOnXaxis();
                    }
                    else if (rows - prev.y >= this.battleShipsGRID.tiles[selected.x, selected.y].ships)
                    {
                        addTilesOnYaxis();
                    }
                    else
                    {
                        prev.clear();
                    }
                }else if (Math.Abs(((Tile)sender).x - prev.x) < Math.Abs(((Tile)sender).y - prev.y) && (((Tile)sender).y - prev.y) < 0)
                {
                    addTilesOnNegativeYaxis();

                }else if (Math.Abs(((Tile)sender).x - prev.x) > Math.Abs(((Tile)sender).y - prev.y) && (((Tile)sender).x - prev.x) < 0)
                {
                    addTilesOnNegativeXaxis();

                }else if (Math.Abs(((Tile)sender).x - prev.x) == Math.Abs(((Tile)sender).y - prev.y) && (((Tile)sender).x - prev.x) < 0)
                {
                    addTilesOnNegativeXaxis();
                }

                if (selected.getType() == typeof(PatrolBoat)) { this.patrolNumberLabel.Text = battleShipsGRID.tiles[selected.x, selected.y].ships.ToString(); }
                if (selected.getType() == typeof(CruiserBoat)) { this.cruiserNumberLabel.Text = battleShipsGRID.tiles[selected.x, selected.y].ships.ToString(); }
                if (selected.getType() == typeof(ValvetShellBoat)) { this.valvetNumberLabel.Text = battleShipsGRID.tiles[selected.x, selected.y].ships.ToString(); }
                if (selected.getType() == typeof(RedCrowBoat)) { this.redNumberLabel.Text = battleShipsGRID.tiles[selected.x, selected.y].ships.ToString(); }
                if (selected.getType() == typeof(VDragonBoat)) { this.dragonNumberLabel.Text = battleShipsGRID.tiles[selected.x, selected.y].ships.ToString(); }

                
                clicks = 0;

            }
        }

        //
        // Start Button setup
        //
        private void StartGameBTN_Click(object sender, EventArgs e)
        {
            this.startGameBTN.Text = "GAME IS ON";
            this.gameOn = true;
        }

        //
        // BattleShipGrid interaction
        //
        private void select_ship(object sender, MouseEventArgs e)
        {
            selected = ((Tile)sender);
            if (selected.getType() == typeof(Tile))
                selected.clear();
            clicks = 0;
        }


        //
        //Additional methods
        //


        //
        //Add tiles on the X axis
        //
        public void addTilesOnXaxis()
        {
            
            if (this.battleShipsGRID.tiles[selected.x, selected.y].ships > 0)
            {
                bool free = true;
                if (prev.x + prev.SQsize <= columns)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {

                        if (this.personalGrid.tiles[prev.x + i, prev.y].state != TileState.UNTOUCHED)
                        {
                            free = false;
                            break;
                        }

                    }
                }
                else
                {
                    prev.clear();
                    return;
                }



                if (free == true)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {
                        if (prev.x + prev.SQsize <= columns)
                        {
                            this.personalGrid.tiles[prev.x + i, prev.y].inheritTileInfo(selected);
                            this.personalGrid.tiles[prev.x + i, prev.y].state = TileState.OCCUPIED;
                        }

                    }
                    this.battleShipsGRID.tiles[selected.x, selected.y].ships--;

                }
                else
                {
                    prev.clear();
                    clicks = 0;
                    MessageBox.Show("You can't place boats on top of eachother...");

                    //dialog box
                }

            }
        }


        //
        //Add tiles on the Y axis
        //
        public void addTilesOnYaxis()
        {
            if (this.battleShipsGRID.tiles[selected.x, selected.y].ships > 0)
            {
                bool free = true;
                if (prev.y + prev.SQsize <= rows)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {

                        if (this.personalGrid.tiles[prev.x, prev.y + i].state != TileState.UNTOUCHED)
                        {
                            free = false;
                            break;
                        }

                    }
                }
                else
                {
                    prev.clear();
                    return;
                }



                if (free == true)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {
                        if (prev.y + prev.SQsize <= rows)
                        {
                            this.personalGrid.tiles[prev.x, prev.y+i].inheritTileInfo(selected);
                            this.personalGrid.tiles[prev.x, prev.y+i].state = TileState.OCCUPIED;

                        }

                    }
                    this.battleShipsGRID.tiles[selected.x, selected.y].ships--;

                }
                else
                {
                    prev.clear();
                    clicks = 0;
                    MessageBox.Show("You can't place boats on top of eachother...");

                    //dialog box
                }
            }
        }


        //
        //Add tiles on the Negativ X axis
        //
        public void addTilesOnNegativeXaxis()
        {
            if (this.battleShipsGRID.tiles[selected.x, selected.y].ships > 0)
            {
                bool free = true;
                if (prev.x - prev.SQsize >= -1)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {

                        if (this.personalGrid.tiles[prev.x - i, prev.y].state != TileState.UNTOUCHED)
                        {
                            free = false;
                            break;
                        }

                    }
                }
                else
                {
                    prev.clear();
                    return;
                }



                if (free == true)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {
                        if (prev.x - prev.SQsize >= -1 )
                        {
                            this.personalGrid.tiles[prev.x - i, prev.y].inheritTileInfo(selected);
                            this.personalGrid.tiles[prev.x - i, prev.y].state = TileState.OCCUPIED;
                        }

                    }
                    this.battleShipsGRID.tiles[selected.x, selected.y].ships--;

                }
                else
                {
                    prev.clear();
                    clicks = 0;
                    MessageBox.Show("You can't place boats on top of eachother...");
                    //dialog box
                }

            }
        }


        //
        //Add tiles on the Negativ Y axis
        //
        public void addTilesOnNegativeYaxis()
        {
            if (this.battleShipsGRID.tiles[selected.x, selected.y].ships > 0)
            {
                bool free = true;
                if (prev.y - prev.SQsize >= -1)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {

                        if (this.personalGrid.tiles[prev.x, prev.y - i].state != TileState.UNTOUCHED)
                        {
                            free = false;
                            break;
                        }

                    }
                }
                else
                {
                    prev.clear();
                    return;
                }



                if (free == true)
                {
                    for (int i = 0; i < prev.SQsize; i++)
                    {
                        if (prev.y - prev.SQsize >= -1)
                        {
                            this.personalGrid.tiles[prev.x, prev.y - i].inheritTileInfo(selected);
                            this.personalGrid.tiles[prev.x, prev.y - i].state = TileState.OCCUPIED;
                        }

                    }
                    this.battleShipsGRID.tiles[selected.x, selected.y].ships--;

                }
                else
                {
                    prev.clear();
                    clicks = 0;
                    MessageBox.Show("You can't place boats on top of eachother...");
                    //dialog box
                }

            }
        }


        public void sendGridInfoToServer()
        {
            for(int i=0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {

                }
            }
        }

        //
        // EXIT APPLICATION
        //
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //client.WriteLine(string.Format("player: {0} disconnected!", YourGrid.Text));
            Application.Exit();
        }
    }

    

    
}
