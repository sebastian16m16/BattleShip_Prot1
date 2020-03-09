namespace BattleShips
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private BattleShips.Resources.Models.TileGrid personalGrid ;
        private BattleShips.Resources.Models.TileGrid opponentGrid ;
        private BattleShips.Resources.Models.TileGrid battleShipsGRID ;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.YourGrid = new System.Windows.Forms.Label();
            this.EnemyGrid = new System.Windows.Forms.Label();
            this.sysMsgs = new System.Windows.Forms.Label();
            this.patrolLabel = new System.Windows.Forms.Label();
            this.cruiserLabel = new System.Windows.Forms.Label();
            this.valvetLabel = new System.Windows.Forms.Label();
            this.redLabel = new System.Windows.Forms.Label();
            this.dragonLabel = new System.Windows.Forms.Label();
            this.dragonNumberLabel = new System.Windows.Forms.Label();
            this.patrolNumberLabel = new System.Windows.Forms.Label();
            this.redNumberLabel = new System.Windows.Forms.Label();
            this.cruiserNumberLabel = new System.Windows.Forms.Label();
            this.valvetNumberLabel = new System.Windows.Forms.Label();
            this.dragonSizeLabel = new System.Windows.Forms.Label();
            this.redSizeLabel = new System.Windows.Forms.Label();
            this.valvetSizeLabel = new System.Windows.Forms.Label();
            this.cruiserSizeLabel = new System.Windows.Forms.Label();
            this.patrolSizeLabel = new System.Windows.Forms.Label();
            this.startGameBTN = new System.Windows.Forms.Button();
            this.opponentGrid = new BattleShips.Resources.Models.TileGrid();
            this.battleShipsGRID = new BattleShips.Resources.Models.TileGrid();
            this.personalGrid = new BattleShips.Resources.Models.TileGrid();
            this.connectedPictureBox = new System.Windows.Forms.PictureBox();
            this.connectedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.connectedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // YourGrid
            // 
            this.YourGrid.AutoSize = true;
            this.YourGrid.Location = new System.Drawing.Point(0, 0);
            this.YourGrid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.YourGrid.Name = "YourGrid";
            this.YourGrid.Size = new System.Drawing.Size(35, 13);
            this.YourGrid.TabIndex = 3;
            this.YourGrid.Text = "label1";
            // 
            // EnemyGrid
            // 
            this.EnemyGrid.AutoSize = true;
            this.EnemyGrid.Location = new System.Drawing.Point(0, 0);
            this.EnemyGrid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.EnemyGrid.Name = "EnemyGrid";
            this.EnemyGrid.Size = new System.Drawing.Size(35, 13);
            this.EnemyGrid.TabIndex = 4;
            this.EnemyGrid.Text = "label1";
            // 
            // sysMsgs
            // 
            this.sysMsgs.AutoSize = true;
            this.sysMsgs.Location = new System.Drawing.Point(0, 0);
            this.sysMsgs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sysMsgs.Name = "sysMsgs";
            this.sysMsgs.Size = new System.Drawing.Size(35, 13);
            this.sysMsgs.TabIndex = 5;
            this.sysMsgs.Text = "label1";
            // 
            // patrolLabel
            // 
            this.patrolLabel.AutoSize = true;
            this.patrolLabel.Location = new System.Drawing.Point(428, 51);
            this.patrolLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patrolLabel.Name = "patrolLabel";
            this.patrolLabel.Size = new System.Drawing.Size(35, 13);
            this.patrolLabel.TabIndex = 6;
            this.patrolLabel.Text = "label1";
            // 
            // cruiserLabel
            // 
            this.cruiserLabel.AutoSize = true;
            this.cruiserLabel.Location = new System.Drawing.Point(428, 86);
            this.cruiserLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cruiserLabel.Name = "cruiserLabel";
            this.cruiserLabel.Size = new System.Drawing.Size(35, 13);
            this.cruiserLabel.TabIndex = 7;
            this.cruiserLabel.Text = "label2";
            // 
            // valvetLabel
            // 
            this.valvetLabel.AutoSize = true;
            this.valvetLabel.Location = new System.Drawing.Point(428, 130);
            this.valvetLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.valvetLabel.Name = "valvetLabel";
            this.valvetLabel.Size = new System.Drawing.Size(35, 13);
            this.valvetLabel.TabIndex = 8;
            this.valvetLabel.Text = "label3";
            // 
            // redLabel
            // 
            this.redLabel.AutoSize = true;
            this.redLabel.Location = new System.Drawing.Point(428, 166);
            this.redLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.redLabel.Name = "redLabel";
            this.redLabel.Size = new System.Drawing.Size(35, 13);
            this.redLabel.TabIndex = 9;
            this.redLabel.Text = "label4";
            // 
            // dragonLabel
            // 
            this.dragonLabel.AutoSize = true;
            this.dragonLabel.Location = new System.Drawing.Point(428, 202);
            this.dragonLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dragonLabel.Name = "dragonLabel";
            this.dragonLabel.Size = new System.Drawing.Size(35, 13);
            this.dragonLabel.TabIndex = 10;
            this.dragonLabel.Text = "label5";
            // 
            // dragonNumberLabel
            // 
            this.dragonNumberLabel.AutoSize = true;
            this.dragonNumberLabel.Location = new System.Drawing.Point(328, 242);
            this.dragonNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dragonNumberLabel.Name = "dragonNumberLabel";
            this.dragonNumberLabel.Size = new System.Drawing.Size(35, 13);
            this.dragonNumberLabel.TabIndex = 15;
            this.dragonNumberLabel.Text = "label6";
            // 
            // patrolNumberLabel
            // 
            this.patrolNumberLabel.AutoSize = true;
            this.patrolNumberLabel.Location = new System.Drawing.Point(322, 60);
            this.patrolNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patrolNumberLabel.Name = "patrolNumberLabel";
            this.patrolNumberLabel.Size = new System.Drawing.Size(41, 13);
            this.patrolNumberLabel.TabIndex = 11;
            this.patrolNumberLabel.Text = "label10";
            // 
            // redNumberLabel
            // 
            this.redNumberLabel.AutoSize = true;
            this.redNumberLabel.Location = new System.Drawing.Point(328, 202);
            this.redNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.redNumberLabel.Name = "redNumberLabel";
            this.redNumberLabel.Size = new System.Drawing.Size(35, 13);
            this.redNumberLabel.TabIndex = 14;
            this.redNumberLabel.Text = "label7";
            // 
            // cruiserNumberLabel
            // 
            this.cruiserNumberLabel.AutoSize = true;
            this.cruiserNumberLabel.Location = new System.Drawing.Point(322, 105);
            this.cruiserNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cruiserNumberLabel.Name = "cruiserNumberLabel";
            this.cruiserNumberLabel.Size = new System.Drawing.Size(35, 13);
            this.cruiserNumberLabel.TabIndex = 12;
            this.cruiserNumberLabel.Text = "label9";
            // 
            // valvetNumberLabel
            // 
            this.valvetNumberLabel.AutoSize = true;
            this.valvetNumberLabel.Location = new System.Drawing.Point(328, 145);
            this.valvetNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.valvetNumberLabel.Name = "valvetNumberLabel";
            this.valvetNumberLabel.Size = new System.Drawing.Size(35, 13);
            this.valvetNumberLabel.TabIndex = 13;
            this.valvetNumberLabel.Text = "label8";
            // 
            // dragonSizeLabel
            // 
            this.dragonSizeLabel.AutoSize = true;
            this.dragonSizeLabel.Location = new System.Drawing.Point(428, 384);
            this.dragonSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dragonSizeLabel.Name = "dragonSizeLabel";
            this.dragonSizeLabel.Size = new System.Drawing.Size(35, 13);
            this.dragonSizeLabel.TabIndex = 20;
            this.dragonSizeLabel.Text = "label5";
            // 
            // redSizeLabel
            // 
            this.redSizeLabel.AutoSize = true;
            this.redSizeLabel.Location = new System.Drawing.Point(428, 348);
            this.redSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.redSizeLabel.Name = "redSizeLabel";
            this.redSizeLabel.Size = new System.Drawing.Size(35, 13);
            this.redSizeLabel.TabIndex = 19;
            this.redSizeLabel.Text = "label4";
            // 
            // valvetSizeLabel
            // 
            this.valvetSizeLabel.AutoSize = true;
            this.valvetSizeLabel.Location = new System.Drawing.Point(428, 312);
            this.valvetSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.valvetSizeLabel.Name = "valvetSizeLabel";
            this.valvetSizeLabel.Size = new System.Drawing.Size(35, 13);
            this.valvetSizeLabel.TabIndex = 18;
            this.valvetSizeLabel.Text = "label3";
            // 
            // cruiserSizeLabel
            // 
            this.cruiserSizeLabel.AutoSize = true;
            this.cruiserSizeLabel.Location = new System.Drawing.Point(428, 268);
            this.cruiserSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cruiserSizeLabel.Name = "cruiserSizeLabel";
            this.cruiserSizeLabel.Size = new System.Drawing.Size(35, 13);
            this.cruiserSizeLabel.TabIndex = 17;
            this.cruiserSizeLabel.Text = "label2";
            // 
            // patrolSizeLabel
            // 
            this.patrolSizeLabel.AutoSize = true;
            this.patrolSizeLabel.Location = new System.Drawing.Point(428, 233);
            this.patrolSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patrolSizeLabel.Name = "patrolSizeLabel";
            this.patrolSizeLabel.Size = new System.Drawing.Size(35, 13);
            this.patrolSizeLabel.TabIndex = 16;
            this.patrolSizeLabel.Text = "label1";
            // 
            // startGameBTN
            // 
            this.startGameBTN.Location = new System.Drawing.Point(546, 440);
            this.startGameBTN.Name = "startGameBTN";
            this.startGameBTN.Size = new System.Drawing.Size(75, 23);
            this.startGameBTN.TabIndex = 21;
            this.startGameBTN.Text = "Start Game";
            this.startGameBTN.UseVisualStyleBackColor = true;
            this.startGameBTN.Click += new System.EventHandler(this.StartGameBTN_Click);
            // 
            // opponentGrid
            // 
            this.opponentGrid.gridSize = new System.Drawing.Size(0, 0);
            this.opponentGrid.Location = new System.Drawing.Point(491, 51);
            this.opponentGrid.Margin = new System.Windows.Forms.Padding(2);
            this.opponentGrid.Name = "opponentGrid";
            this.opponentGrid.Size = new System.Drawing.Size(324, 282);
            this.opponentGrid.TabIndex = 1;
            this.opponentGrid.tiles = null;
            this.opponentGrid.tilesImage = null;
            // 
            // battleShipsGRID
            // 
            this.battleShipsGRID.gridSize = new System.Drawing.Size(0, 0);
            this.battleShipsGRID.Location = new System.Drawing.Point(368, 51);
            this.battleShipsGRID.Margin = new System.Windows.Forms.Padding(2);
            this.battleShipsGRID.Name = "battleShipsGRID";
            this.battleShipsGRID.Size = new System.Drawing.Size(56, 282);
            this.battleShipsGRID.TabIndex = 2;
            this.battleShipsGRID.tiles = null;
            this.battleShipsGRID.tilesImage = null;
            // 
            // personalGrid
            // 
            this.personalGrid.gridSize = new System.Drawing.Size(0, 0);
            this.personalGrid.Location = new System.Drawing.Point(9, 51);
            this.personalGrid.Margin = new System.Windows.Forms.Padding(2);
            this.personalGrid.Name = "personalGrid";
            this.personalGrid.Size = new System.Drawing.Size(306, 282);
            this.personalGrid.TabIndex = 0;
            this.personalGrid.tiles = null;
            this.personalGrid.tilesImage = null;
            // 
            // connectedPictureBox
            // 
            this.connectedPictureBox.Location = new System.Drawing.Point(658, 413);
            this.connectedPictureBox.Name = "connectedPictureBox";
            this.connectedPictureBox.Size = new System.Drawing.Size(100, 50);
            this.connectedPictureBox.TabIndex = 22;
            this.connectedPictureBox.TabStop = false;
            // 
            // connectedLabel
            // 
            this.connectedLabel.AutoSize = true;
            this.connectedLabel.Location = new System.Drawing.Point(765, 440);
            this.connectedLabel.Name = "connectedLabel";
            this.connectedLabel.Size = new System.Drawing.Size(35, 13);
            this.connectedLabel.TabIndex = 23;
            this.connectedLabel.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 527);
            this.Controls.Add(this.connectedLabel);
            this.Controls.Add(this.connectedPictureBox);
            this.Controls.Add(this.startGameBTN);
            this.Controls.Add(this.dragonSizeLabel);
            this.Controls.Add(this.redSizeLabel);
            this.Controls.Add(this.valvetSizeLabel);
            this.Controls.Add(this.cruiserSizeLabel);
            this.Controls.Add(this.patrolSizeLabel);
            this.Controls.Add(this.dragonNumberLabel);
            this.Controls.Add(this.dragonLabel);
            this.Controls.Add(this.redNumberLabel);
            this.Controls.Add(this.patrolNumberLabel);
            this.Controls.Add(this.valvetNumberLabel);
            this.Controls.Add(this.cruiserNumberLabel);
            this.Controls.Add(this.redLabel);
            this.Controls.Add(this.valvetLabel);
            this.Controls.Add(this.cruiserLabel);
            this.Controls.Add(this.patrolLabel);
            this.Controls.Add(this.opponentGrid);
            this.Controls.Add(this.battleShipsGRID);
            this.Controls.Add(this.sysMsgs);
            this.Controls.Add(this.EnemyGrid);
            this.Controls.Add(this.YourGrid);
            this.Controls.Add(this.personalGrid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.connectedPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label YourGrid;
        private System.Windows.Forms.Label EnemyGrid;
        private System.Windows.Forms.Label sysMsgs;
        private System.Windows.Forms.Label dragonNumberLabel;
        private System.Windows.Forms.Label patrolNumberLabel;
        private System.Windows.Forms.Label redNumberLabel;
        private System.Windows.Forms.Label cruiserNumberLabel;
        private System.Windows.Forms.Label valvetNumberLabel;
        private System.Windows.Forms.Label patrolLabel;
        private System.Windows.Forms.Label cruiserLabel;
        private System.Windows.Forms.Label valvetLabel;
        private System.Windows.Forms.Label redLabel;
        private System.Windows.Forms.Label dragonLabel;
        private System.Windows.Forms.Label dragonSizeLabel;
        private System.Windows.Forms.Label redSizeLabel;
        private System.Windows.Forms.Label valvetSizeLabel;
        private System.Windows.Forms.Label cruiserSizeLabel;
        private System.Windows.Forms.Label patrolSizeLabel;
        private System.Windows.Forms.Button startGameBTN;
        private System.Windows.Forms.PictureBox connectedPictureBox;
        private System.Windows.Forms.Label connectedLabel;
    }
}