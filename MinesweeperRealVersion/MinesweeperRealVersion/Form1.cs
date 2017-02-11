using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperRealVersion
{
    public partial class Minesweeper : Form
    {
        private GameBoard board;
        public static Minesweeper currentBoard;

        public Minesweeper()
        {
            InitializeComponent();
            board = new GameBoard(10, 10, GameBoard.Difficulty.Easy);
            board.GenerateTest();
            Console.WriteLine(board.ToString());
            currentBoard = this;
            PictureBoxWithTile bas = new PictureBoxWithTile();
            Bitmap bmp = bas.UnclickedImage;
            PictureBoxWithTile pbwt = new PictureBoxWithTile(new Point(0, 0), bmp, "C:/Users/user/Pictures/Minesweeper/empty.png");
            PictureBoxWithTile.Test(pbwt);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
