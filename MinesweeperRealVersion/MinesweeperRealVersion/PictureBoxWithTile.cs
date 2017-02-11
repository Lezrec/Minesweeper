using System;
using System.Windows.Forms;
using System.Drawing;

namespace MinesweeperRealVersion
{
    public class PictureBoxWithTile : PictureBox 
    {

        //TODO MAKE ALL THESE PROPERTIES
        //if you've cloned this repo, get your own images you filthy cheater
        public  Bitmap flagImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/flagged.png");
        public  Bitmap emptyImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/empty.png"); //<- WE NEED THIS
        public  Bitmap bombImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/bomb.png");
        public  Bitmap oneImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/1.png");
        public  Bitmap twoImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/2.png");
        public  Bitmap threeImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/3.png");
        public  Bitmap fourImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/4.png");
        public  Bitmap fiveImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/5.png");
        public  Bitmap sixImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/6.png");
        public  Bitmap sevenImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/7.png");
        public  Bitmap eightImage = new Bitmap("C:/Users/user/Pictures/Minesweeper/8.png");


        //YO WE FINNA NEED TO MAKE SURE THIS ISNT EMPTY BUT UNCLICKED OK?
        public Bitmap UnclickedImage
        {
            get { return new Bitmap("C:/Users/user/Pictures/Minesweeper/empty.png"); }
        }


        private Tile myAttachedTile;
        public PictureBoxWithTile(GameBoard board, int x, int y) : base()
        {
            myAttachedTile = board.tiles[y, x];
            Size = new System.Drawing.Size(32, 32);
            AssignPicture();
            Enabled = true;
            MouseClick += ParseClick;
            
        }

        //testing constructor
        internal PictureBoxWithTile(Point leftPos ,Bitmap bmp, string dir) : base()
        {
            this.Location = leftPos;
            ImageLocation = dir;
            Size = new System.Drawing.Size(32, 32);
            Enabled = true;
        }

        //testing constructor for generation
        internal PictureBoxWithTile() : base()
        {

        }

        public static void Test(PictureBoxWithTile pb)
        {
            Minesweeper.currentBoard.Controls.Add(pb);
        }

        private void ParseClick(object sender, MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Left:
                    if (myAttachedTile.Unclicked)
                    {
                        //TODO: Select Image
                        myAttachedTile.Reveal(); //<- does revealing in the tile class really require a method, or just a property?
                    }
                    //do nothing otherwise
                    break;
                case MouseButtons.Right:
                    if (myAttachedTile.Unclicked)
                    {
                        if (myAttachedTile.Flagged)
                        {
                            //unflag tile
                            Image = UnclickedImage;
                            myAttachedTile.Flagged = false;
                        }
                        else
                        {
                            Image = flagImage;
                            myAttachedTile.Flagged = true;
                        }
                    }
                    //do nothing otherwise
                    break;
                default:
                    //do nothing prob
                    break;
            }
        }

        //should call on any change (revealed, flagged)
        private void AssignPicture()
        {
            

            if (myAttachedTile.Unclicked)
            {
                if (myAttachedTile.Flagged)
                {
                    //flagged
                    Image = flagImage;
                }
                else
                {
                    //unclicked
                    Image = UnclickedImage;
                }
            }
            else
            {
                if (myAttachedTile is EmptyTile)
                {
                    //displays jack shit
                    Image = emptyImage;
                }
                else if (myAttachedTile is BombTile)
                {
                    //displays bomb
                    Image = bombImage;
                }
                else if (myAttachedTile is ClickedTileWithNumber)
                {
                    
                    //displays number
                    switch(((ClickedTileWithNumber)myAttachedTile).AmountOfAdjacentBombs)
                    {
                        case 1:
                            Image = oneImage;
                            break;
                        case 2:
                            Image = twoImage;
                            break;
                        case 3:
                            Image = threeImage;
                            break;
                        case 4:
                            Image = fourImage;
                            break;
                        case 5:
                            Image = fiveImage;
                            break;
                        case 6:
                            Image = sixImage;
                            break;
                        case 7:
                            Image = sevenImage;
                            break;
                        case 8:
                            Image = eightImage;
                            break;
                        default:
                            goto YouFuckedUp;
                            //HOW THE FUCK DID YOU MESS THIS UP
                        YouFuckedUp:

                            break;
                    }

                }
            }
            
        }
    }
}
