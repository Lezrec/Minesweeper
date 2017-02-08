using System;
using System.Windows.Forms;
using System.Drawing;

namespace MinesweeperRealVersion
{
    public class PictureBoxWithTile : PictureBox 
    {
        //if you've cloned this repo, get your own images you filthy cheater
        public Bitmap flagImage = new Bitmap("flagged.bmp");
        public Bitmap emptyImage = new Bitmap("empty.bmp");
        public Bitmap unclickedImage = new Bitmap("unclicked.bmp");
        public Bitmap bombImage = new Bitmap("bomb.bmp");
        public Bitmap oneImage = new Bitmap("1.bmp");
        public Bitmap twoImage = new Bitmap("2.bmp");
        public Bitmap threeImage = new Bitmap("3.bmp");
        public Bitmap fourImage = new Bitmap("4.bmp");
        public Bitmap fiveImage = new Bitmap("5.bmp");
        public Bitmap sixImage = new Bitmap("6.bmp");
        public Bitmap sevenImage = new Bitmap("7.bmp");
        public Bitmap eightImage = new Bitmap("8.bmp");


        private Tile myAttachedTile;
        public PictureBoxWithTile(GameBoard board, int x, int y) : base()
        {
            myAttachedTile = board.tiles[y, x];
            Size = new System.Drawing.Size(32, 32);
            AssignPicture();
            Enabled = true;
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
                    Image = unclickedImage;
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
