using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MinesweeperRealVersion
{
    public abstract class Tile
    {
        protected GameBoard board;
        public bool Unclicked { get; set; }
        public bool Flagged { get; set; }
        protected int positionX;
        protected int positionY;

        public int X {
            get { return positionX;; }
            }
        public int Y {
            get { return positionY; }
        }

        public GameBoard Board
        {
            get { return Board; }
        }

        public Tile(int x, int y)
        {
            positionX = x;
            positionY = y;
        }

        public virtual void CallThisOnMeOnceEverythingIsFinished(GameBoard gameboard)
        {
            board = gameboard;
        }
    }

    public class EmptyTile : Tile
    {
        public EmptyTile(int x, int y) : base(x,y)
        {
            
        }
    }

    public class BombTile : Tile
    {
        public BombTile(int x, int y) : base(x,y)
        {
            
        }
    }

    //public class UnclickedTile : Tile
    //{
    //    public bool Flagged { get; set; }

    //    public UnclickedTile(int x, int y) : base(x,y)
    //    {

    //    }
    //}

    public class ClickedTileWithNumber : Tile
    {
        //1 to 8
        private int amtBombsNearMe;// Set by Gameboard.GetAdjacentTiles(this, out amt);
        private List<Tile> myBombsNearMe; //Returned by Gameboard.GetAdjacentTileS(this, out amt);

        public int AmountOfAdjacentBombs { get { return amtBombsNearMe; } }


        public ClickedTileWithNumber(int x, int y) : base(x, y)
        {

        }

        public ClickedTileWithNumber(int x, int y, int amountOfBombs) : base(x, y) //debug
        {
            amtBombsNearMe = amountOfBombs;
        }

        public override void CallThisOnMeOnceEverythingIsFinished(GameBoard board)
        {
            base.CallThisOnMeOnceEverythingIsFinished(board);
            myBombsNearMe = board.GetAdjacentBombs(this, out amtBombsNearMe);
        }


        public bool Equals(Tile other)
        {
            return GameBoard.IsBasicallyTheSameTile(this, other);
        }


    }
    
}
