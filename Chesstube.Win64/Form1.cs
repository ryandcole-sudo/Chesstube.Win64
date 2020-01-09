using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chesstube.Win64
{
    public partial class Form1 : Form
    {
        public delegate void WinGame();
        public event WinGame win;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void chessBoard1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chessBoard1.new_game();
        }

        private void scoreSheet1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void scoreSheet1_SizeChanged(object sender, EventArgs e)
        {
               
        }

        private void chessBoard1_SizeChanged(object sender, EventArgs e)
        {
            scoreSheet1.Size = new Size(chessBoard1.Size.Width/2, scoreSheet1.Height);
            //scoreSheet1.Location = new Point(chessBoard1.Size.Width / 2, 0);
        }

       private void chessBoard1_MoveMade(object sender, EventArgs e)
        {
            scoreSheet1.Text = "White: Ji Hu\n";
            scoreSheet1.Text += "Black: Kin Fi\n\n";
            Board board = new Board();
            board.setup_fen(chessBoard1.FenString);
            for(int i=0; i < chessBoard1.MoveList.ToArray().Length;i++)
            {
                int l = chessBoard1.MoveList.ToArray().Length-1;
                int[] mv = chessBoard1.MoveList.ToArray()[l-i];
                String move = PGNReader.convertMove(board, mv);
   
                if (i % 2 == 0)
                    scoreSheet1.Text += (i / 2 + 1) + "." + move;
                else
                    scoreSheet1.Text += move;

                scoreSheet1.Text += " ";
                board.Move(mv[0], mv[1]);
            }
            
        }
    }
}