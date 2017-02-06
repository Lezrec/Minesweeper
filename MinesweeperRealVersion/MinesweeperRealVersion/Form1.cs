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
    public partial class Form1 : Form
    {
        private GameBoard board;

        public Form1()
        {
            InitializeComponent();
            board = new GameBoard(10, 10, GameBoard.Difficulty.Easy);
            board.GenerateTest();
            Console.WriteLine(board.ToString());

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
