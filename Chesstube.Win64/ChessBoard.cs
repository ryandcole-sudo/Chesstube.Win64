using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Chesstube.Win64
{
    public partial class ChessBoard : UserControl
    {
        Board board = new Board();

        GameMode gameMode = GameMode.PlayGame;

        public int selected_square = -1;
        
        public enum GameMode
        {
            Null,PlayGame,SetupBoard
        }

        public ChessBoard()
        {
            InitializeComponent();
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {

            new_game();
               
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
     
        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) //Left click to select square and make moves
            {
                Point p = new Point(e.X, e.Y);
                int f = (e.X*8) / tableLayoutPanel1.Height;
                int c = (e.Y*8) / tableLayoutPanel1.Height;

                int x = (7-c) * 8 + f;

                if (selected_square > 0)
                {
                    board.Move(selected_square, x);
                    selected_square = -1;
                }
                else
                {
                    selected_square = x;
                }

                int size = tableLayoutPanel1.Height; //Assume height=width(that should be true);

                int size8 = size / 8; // one-eigth the size of the board(The size of the board on the square)

                Point loc = new Point(size8 * f, size8 * c);  //Location of the square

                Size siz = new Size(size8, size8);

                Rectangle square = new Rectangle(loc, siz);

                Graphics g = tableLayoutPanel1.CreateGraphics();
                paint_full(g);

            }
           if( e.Button == MouseButtons.Right) //Right Click to show context menu
           { 
                Point p = new Point(e.X, e.Y);
                p = PointToScreen(p);
                contextMenuStrip1.Show(p);
           }

        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

            paint_cell(e.Graphics, e.Row, e.Column);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           Graphics g = tableLayoutPanel1.CreateGraphics();
           paint_cell(g, 0, 0);

        }

        protected void paint_cell(Graphics g, int col, int row)
        {

            int size = tableLayoutPanel1.Height; //Assume height=width(that should be true);

            int size8 = size / 8; // one-eigth the size of the board(The size of the board on the square)

            Point loc = new Point(size8 * col, size8 * row);  //Location of the square

            Size siz = new Size(size8, size8);

            Rectangle square = new Rectangle(loc, siz);

            g.FillRectangle(Brushes.White, square); //Clears the cell

            if (col % 2 != row % 2)
            {
                g.FillRectangle(Brushes.Navy, square);

            }




            if (board.hasPiece(Board.ChessPiece.NoPiece))
            {
                //e.Graphics.DrawImage(chessImg.Images[2], square);
            }

            int file = col;
            int rank = 7 - row;

            int sq = 8 * rank + file;

            Board.ChessPiece piece = board.squares[sq];

            switch (piece)
            {
                case Board.ChessPiece.NoPiece: //Do nothing
                    break;
                case Board.ChessPiece.bBishop:
                    g.DrawImage(chessImg.Images[0], square);
                    break;
                case Board.ChessPiece.wBishop:
                    g.DrawImage(chessImg.Images[1], square);
                    break;
                case Board.ChessPiece.bKing:
                    g.DrawImage(chessImg.Images[2], square);
                    break;
                case Board.ChessPiece.wKing:
                    g.DrawImage(chessImg.Images[3], square);
                    break;
                case Board.ChessPiece.bKnight:
                    g.DrawImage(chessImg.Images[4], square);
                    break;
                case Board.ChessPiece.wKnight:
                    g.DrawImage(chessImg.Images[5], square);
                    break;
                case Board.ChessPiece.bPawn:
                    g.DrawImage(chessImg.Images[6], square);
                    break;
                case Board.ChessPiece.wPawn:
                    g.DrawImage(chessImg.Images[7], square);
                    break;
                case Board.ChessPiece.bQueen:
                    g.DrawImage(chessImg.Images[8], square);
                    break;
                case Board.ChessPiece.wQueen:
                    g.DrawImage(chessImg.Images[9], square);
                    break;
                case Board.ChessPiece.bRook:
                    g.DrawImage(chessImg.Images[10], square);
                    break;
                case Board.ChessPiece.wRook:
                    g.DrawImage(chessImg.Images[11], square);
                    break;

            }

            if (col* 8 + col == selected_square)
            {
                g.DrawRectangle(Pens.OrangeRed, square);
            }


        }
        protected void paint_full(Graphics g)
        {
            for(int i = 0; i < 64; i++)
            {
                paint_cell(g, i % 8, i / 8);
            }
        }
        public void set_up(string set_up)
        {
            board.setup_fen(set_up);
        }
        public void new_game()
        {
            board.setup_fen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Qkq - 0 1");
        }
    }

    
}
