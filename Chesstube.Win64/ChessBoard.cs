using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Chesstube.Win64
{
    public partial class ChessBoard : UserControl
    {
        public  Board board = new Board();

        public Stack<int[]> MoveList;
        public List<int[]> UndoList;

        GameMode gameMode = GameMode.PlayGame;

        public String FenString = "";

        public int selected_square = -1;

        public event EventHandler MoveMade; //Whenever a valid move is made on the chessboard

        public enum GameMode
        {
            Null, PlayGame, SetupBoard
        }

        //For Graphics
        Board.ChessPiece[] l_squares = new Board.ChessPiece[64];
        int l_selected = -1;

        public ChessBoard()
        {
            InitializeComponent();
            MoveList = board.MoveList;
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
            new_game();
            Console.WriteLine("FEN: " + PGNReader.convert_FEN(board));   
        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) //Left click to select square and make moves
            {
                Point p = new Point(e.X, e.Y);
                int f = (e.X*8) / tableLayoutPanel1.Height;
                int c = (e.Y*8) / tableLayoutPanel1.Height;

                int x = (7-c) * 8 + f;

                if (selected_square >= 0)
                {

                    bool can_move = false;

                    if (board.canMove(selected_square, x))
                    {
                        
                        can_move = true;
                    }


                    int[] a = new int[2];
                    a[0] = selected_square;
                    a[1] = x;

                   
                    // Form1.ActiveForm.Text = PGNReader.decodeMove(board, "Nc3", a).ToString();
                    //Form.ActiveForm.Text = PGNReader.convertMove(board, a);
                    //board.Move(Form.ActiveForm.Text); //TODO: <<DEBUG

                    board.Move(selected_square, x);

                    if (can_move)
                    {
                        EventArgs evt = new EventArgs();
                        MoveMade(this, evt);
                    }
                  
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

                //Graphics g = tableLayoutPanel1.CreateGraphics();
               // paint_full(g);

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
            paint_full(g);

        }

        protected void paint_cell(Graphics g, int col, int row)
        {
            

            int size = tableLayoutPanel1.Height; //Assume height=width(that should be true);

            int size8 = size / 8; // one-eigth the size of the board(The size of the board on the square)

            Point loc = new Point(size8 * col, size8 * row);  //Location of the square

            Size siz = new Size(size8, size8);

            Rectangle square = new Rectangle(loc, siz);

            int i = col * 8 + row;

            g.FillRectangle(Brushes.White, square); //Clears the cell

            if (col % 2 != row % 2)
            {
                g.FillRectangle(Brushes.Navy, square);

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

            if ( (7-row)* 8 + col == selected_square)
            {
                g.DrawRectangle(Pens.OrangeRed, square);
            }

           
        }
        public void highlight_cell(Graphics g, int col,int row)
        {

            int size = tableLayoutPanel1.Height; //Assume height=width(that should be true);

            int size8 = size / 8; // one-eigth the size of the board(The size of the board on the square)

            Point loc = new Point(size8 * col, size8 * row);  //Location of the square

            Size siz = new Size(size8, size8);

            Rectangle square = new Rectangle(loc, siz);

            g.DrawRectangle(Pens.OrangeRed, square);

        }
        protected void paint_full(Graphics g)
        {


            for (int i = 0; i < 64; i++)
            {
                if (l_squares[i] != board.squares[i])
                {
                    l_squares[i] = board.squares[i];
                    paint_cell(g, i %8,7- i / 8);
                }
                if(l_selected != selected_square&&i==selected_square)
                {
                    l_selected = selected_square;
                    paint_cell(g, i % 8, 7 - i / 8);
                }

            }
            
        }
        public void set_up(string set_up)
        {
            FenString = set_up;
            board.setup_fen(set_up);
        }
        public void new_game()
        {
            set_up("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            selected_square = -1;
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(PGNReader.convert_FEN(board));
        }

        private void pastePositionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
                set_up(Clipboard.GetText());
        }

        


     
    }

    
}
