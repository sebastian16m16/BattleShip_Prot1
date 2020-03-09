using BattleShips.Resources.Models;

namespace BattleShips.Resources.Utilities
{
    internal class EventState
    {
        private TileGrid personalGrid = new TileGrid();
        private TileGrid battleShipsGrid = new TileGrid();
        private byte clicks { get; set; }

        public EventState(TileGrid pGrid, TileGrid bGrid, byte clicks) {
            this.personalGrid.LoadGrid(pGrid.gridSize, pGrid.tilesImage);
            this.battleShipsGrid.LoadGrid(bGrid.gridSize, bGrid.tilesImage);

            this.personalGrid.inheritGrid(pGrid);
            this.battleShipsGrid.inheritGrid(bGrid);

            this.clicks = clicks;
        }

        public TileGrid getLastPersonalGrid()
        {
            return personalGrid;
        }

        public TileGrid getLastBattleshipGrid()
        {
            return battleShipsGrid;
        }

        public byte getClicks()
        {
            return this.clicks;
        }
    }
}