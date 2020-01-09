using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Chesstube
{
   class PGNReader
    {
        public static string convert_FEN(Board board)
        {
            //TODO : Edit this
            string fen = "";

            int j = 0;
            Board.ChessPiece[] copy = new Board.ChessPiece[64];
            permute_square(board.squares, copy);
            for(int i = 63; i >= 0; i--)
            {
                char piece = 'x';
                switch (copy[i])
                {
                    case Board.ChessPiece.wPawn:
                        piece = 'P';
                        break;
                    case Board.ChessPiece.bPawn:
                        piece = 'p';
                        break;
                    case Board.ChessPiece.wRook:
                        piece = 'R';
                        break;
                    case Board.ChessPiece.bRook:
                        piece = 'r';
                        break;
                    case Board.ChessPiece.wKing:
                        piece = 'K';
                        break;
                    case Board.ChessPiece.bKing:
                        piece = 'k';
                        break;
                    case Board.ChessPiece.wKnight:
                        piece = 'N';
                        break;
                    case Board.ChessPiece.bKnight:
                        piece = 'n';
                        break;
                    case Board.ChessPiece.wBishop:
                        piece = 'B';
                        break;
                    case Board.ChessPiece.bBishop:
                        piece = 'b';
                        break;
                    case Board.ChessPiece.wQueen:
                        piece = 'Q';
                        break;
                    case Board.ChessPiece.bQueen:
                        piece = 'q';
                        break;
                    
                }
               
                

                if (i % 8 == 7 && i != 63)
                {
                    if (j > 0)
                    {
                        fen += j;
                        j = 0;
                    }
                    fen += "/";
                }
                if (piece == 'x')
                    j++;
                if (piece != 'x')
                {
                    if (j > 0)
                        fen += j;
                    fen += piece;
                }    
                  
            }
            fen += " ";
            fen += board.blacktomove ? " b " : " w ";
            fen += board.white_king_castle ? "K" : "";
            fen += board.white_queen_castle ? "Q" : "";
            fen += board.black_king_castle ? "k" : "";
            fen += board.black_queen_castle ? "q" : "";

            if (!board.white_king_castle && !board.black_queen_castle && !board.white_queen_castle && !board.black_queen_castle)
                fen += "-";

            if (board.en_passantsquare != 0)
                fen += " "+board.convert(board.en_passantsquare)+" ";
            else
                fen += " - ";
            

            fen += " "+board.fullmove_count+" "; //TODO: Check this
            fen += " " + board.halfmove_count; //TODO:Check this
            
            return fen;
        }
        public static Board.ChessPiece[] decode_FEN(string fen_string) //Decodes a fen string and returns the board position
        {
            Board.ChessPiece[] squares = new Board.ChessPiece[64];
            squares[33] = Board.ChessPiece.NoPiece;
            int i = 63;

            string[] fn_string = fen_string.Split(' ');
            string move_string = fn_string[0];

            
         
            
            //TODO: Make corrections, the board reads fen position right-to-left, instead of left to right as required.

            foreach(char letter in move_string)
            {
                switch (letter)
                {
                    case '/':
                        break;
                    case 'p':
                        squares[i] = Board.ChessPiece.bPawn;
                        i--;
                        break;
                    case 'r':
                        squares[i] = Board.ChessPiece.bRook;
                        i--;
                        break;
                    case 'k':
                        squares[i] = Board.ChessPiece.bKing;
                        i--;
                        break;
                    case 'n':
                        squares[i] = Board.ChessPiece.bKnight;
                        i--;
                        break;
                    case 'b':
                        squares[i] = Board.ChessPiece.bBishop;
                        i--;
                        break;
                    case 'q':
                        squares[i] = Board.ChessPiece.bQueen;
                        i--;
                        break;

                    case 'P':
                        squares[i] = Board.ChessPiece.wPawn;
                        i--;
                        break;
                    case 'R':
                        squares[i] = Board.ChessPiece.wRook;
                        i--;
                        break;
                    case 'K':
                        squares[i] = Board.ChessPiece.wKing;
                        i--;
                        break;
                    case 'N':
                        squares[i] = Board.ChessPiece.wKnight;
                        i--;
                        break;
                    case 'B':
                        squares[i] = Board.ChessPiece.wBishop;
                        i--;
                        break;
                    case 'Q':
                        squares[i] = Board.ChessPiece.wQueen;
                        i--;
                        break;
                    case '1': case '2': case '3': case '4':
                    case '5': case '6': case '7': case '8':
                        i -= letter - '0';
                        break;
                }
            }

            Board.ChessPiece[] copy = new Board.ChessPiece[64];

            permute_square(squares,copy); //
            return copy;
        }

        public static bool FEN_castle(string fen_string,bool[] array_castle)
        {

            string[] fn_string = fen_string.Split(' ');

            string castle_string = fn_string[2];
            if(array_castle.Length < 4)
                return false;

            if (castle_string.Contains("K"))
            {
                array_castle[0] = true;
            }
            if(castle_string.Contains("Q"))
            {
                array_castle[1] = true;
            }
            if(castle_string.Contains("k"))
            {
                array_castle[2] = true;
            }
            if (castle_string.Contains("q"))
            {
                array_castle[3] = true;
            }

            return array_castle[0]||array_castle[1]||array_castle[2]||array_castle[3];
        }

        public static bool FEN_black_to_move(string fen_string) //Reads the FEN String and returns true if its black to move, false otherwise
        {
            string[] fn_string = fen_string.Split(' ');
            string btm_string = fn_string[1];

            switch (btm_string)
            {
                case "b":
                    return true;
                default:
                    return false;
            }
        }
     
        public static bool validate_FEN(string fen_string)
        { 
            //TODO: Edit this
            return false;
        }
        private static void permute_square(Board.ChessPiece[] square, Board.ChessPiece[] copy) //Performs a horizontal flip of the board squares
        {
            Board.ChessPiece[] cpy_sqr = new Board.ChessPiece[100];
            for(int i =0; i < 64; i++)
            {
                int rank = i/8 + 1;
                int file = i%8 + 1;

                int j = (rank-1)*8 + 8 -file; //Find the complementary file
                copy[i] = square[j];

            }
           

        }

        public static bool decodeMove(Board board,string move, int[] move_array)
        {
            String pieces = "([P,B,N,R,Q,K])";
            String square = "([a-h])([1-8])";
            if (move_array.Length != 2)
                return false;

            Regex rex1 = new Regex("O-O"); //King-side Castling
            Regex rex2 = new Regex("O-O-O"); //Queen side castling

           
            Regex rex3 = new Regex(square + "=?" + pieces+"?"); //Pawn moves (e4 e8Q e4=Q)  1m
            Regex rex4 = new Regex("([a-h])x?"+square+"=?"+pieces+"?"); //Pawn capture (dxe8Q dxe8=Q)

            Regex rex5 = new Regex(pieces+"x?"+square); // Piece Moves(Rd4 Rxd4) 2m
            Regex rex6 = new Regex(pieces + "([a-h])x?" + square); // Rad4 Raxd4
            Regex rex7 = new Regex(pieces + "([1-8])x?" + square); //R8d4 R8xd4
            Regex rex8 = new Regex(pieces + square + "x?" +square); //Ra4e4 Ra4xe4
            
            Regex rex9 = new Regex(square + "[x,-]"+square); //Long Algebraic notation

            Match mv;
            if (rex9.IsMatch(move))
            {
                mv = rex9.Match(move);
                String sq_on = mv.Groups[1].Value + mv.Groups[2].Value;
                String sq = mv.Groups[3].Value + mv.Groups[4].Value;

                
                move_array[0] = board.convert(sq_on);
                move_array[1] = board.convert(sq);

                if (board.canMove(move_array[0], move_array[1]))
                    return true;
                else
                    return false;

            }
            if (rex8.IsMatch(move))
            {
                
                mv = rex8.Match(move);
                String p = mv.Groups[1].Value;
                String sq_on = mv.Groups[2].Value + mv.Groups[3].Value;
                String sq = mv.Groups[4].Value + mv.Groups[5].Value;

                Board.ChessPiece piece = Board.ChessPiece.NoPiece;

                switch (p)
                {
                    case "P":
                        piece = board.blacktomove ? Board.ChessPiece.bPawn : Board.ChessPiece.wPawn;
                        break;
                    case "B":
                        piece = board.blacktomove ? Board.ChessPiece.bBishop : Board.ChessPiece.wBishop;
                        break;
                    case "N":
                        piece = board.blacktomove ? Board.ChessPiece.bKnight : Board.ChessPiece.wKnight;
                        break;
                    case "R":
                        piece = board.blacktomove ? Board.ChessPiece.bRook : Board.ChessPiece.wRook;
                        break;
                    case "Q":
                        piece = board.blacktomove ? Board.ChessPiece.bQueen : Board.ChessPiece.wQueen;
                        break;
                    case "K":
                        piece = board.blacktomove ? Board.ChessPiece.bKing : Board.ChessPiece.wKing;
                        break;
                    default:
                        return false;
                }


                move_array[0] = board.convert(sq_on);
                move_array[1] = board.convert(sq);

                if (board.canMove(move_array[0], move_array[1]) && board.hasPiece(move_array[0], piece))
                    return true;
                else
                    return false;

            }
            if (rex7.IsMatch(move))
            {
                mv = rex7.Match(move);

                String p = mv.Groups[1].Value;
                int f = mv.Groups[2].Value[0] - '0';
                String sq = mv.Groups[3].Value + mv.Groups[4];

                Board.ChessPiece piece = Board.ChessPiece.NoPiece;
                switch (p)
                {
                    case "P":
                        piece = board.blacktomove ? Board.ChessPiece.bPawn : Board.ChessPiece.wPawn;
                        break;
                    case "B":
                        piece = board.blacktomove ? Board.ChessPiece.bBishop : Board.ChessPiece.wBishop;
                        break;
                    case "N":
                        piece = board.blacktomove ? Board.ChessPiece.bKnight : Board.ChessPiece.wKnight;
                        break;
                    case "R":
                        piece = board.blacktomove ? Board.ChessPiece.bRook : Board.ChessPiece.wRook;
                        break;
                    case "Q":
                        piece = board.blacktomove ? Board.ChessPiece.bQueen : Board.ChessPiece.wQueen;
                        break;
                    case "K":
                        piece = board.blacktomove ? Board.ChessPiece.bKing : Board.ChessPiece.wKing;
                        break;
                    default:
                        return false;
                }

                move_array[1] = board.convert(sq);

                for (int i = 0; i < 63; i++)
                {
                    if (board.canMove(i, move_array[1]) && board.hasPiece(i, piece) && board.fileof(i) == f)
                    {
                        move_array[0] = i;
                        return true;
                    }
                }

                return false;
            }
            if (rex6.IsMatch(move))
            {
                mv = rex6.Match(move);

                String p = mv.Groups[1].Value;
                int r = mv.Groups[2].Value[0] - 'a';
                String sq = mv.Groups[3].Value + mv.Groups[4];

                Board.ChessPiece piece = Board.ChessPiece.NoPiece;
                switch (p)
                {
                    case "P":
                        piece = board.blacktomove ? Board.ChessPiece.bPawn : Board.ChessPiece.wPawn;
                        break;
                    case "B":
                        piece = board.blacktomove ? Board.ChessPiece.bBishop : Board.ChessPiece.wBishop;
                        break;
                    case "N":
                        piece = board.blacktomove ? Board.ChessPiece.bKnight : Board.ChessPiece.wKnight;
                        break;
                    case "R":
                        piece = board.blacktomove ? Board.ChessPiece.bRook : Board.ChessPiece.wRook;
                        break;
                    case "Q":
                        piece = board.blacktomove ? Board.ChessPiece.bQueen : Board.ChessPiece.wQueen;
                        break;
                    case "K":
                        piece = board.blacktomove ? Board.ChessPiece.bKing : Board.ChessPiece.wKing;
                        break;
                    default:
                        return false;
                }

                move_array[1] = board.convert(sq);

                for (int i = 0; i < 63; i++)
                {
                    if (board.canMove(i, move_array[1]) && board.hasPiece(i, piece) && board.rankof(i) == r)
                    {
                        move_array[0] = i;
                        return true;
                    }
                }

                return false;
            }
            if (rex5.IsMatch(move))
            {
                mv = rex5.Match(move); //3
                String p = mv.Groups[1].Value;
                String sq = mv.Groups[2].Value + mv.Groups[3];

                Board.ChessPiece piece = Board.ChessPiece.NoPiece;
                switch (p)
                {
                    case "P":
                        piece = board.blacktomove ? Board.ChessPiece.bPawn : Board.ChessPiece.wPawn;
                        break;
                    case "B":
                        piece = board.blacktomove ? Board.ChessPiece.bBishop : Board.ChessPiece.wBishop;
                        break;
                    case "N":
                        piece = board.blacktomove ? Board.ChessPiece.bKnight : Board.ChessPiece.wKnight;
                        break;
                    case "R":
                        piece = board.blacktomove ? Board.ChessPiece.bRook : Board.ChessPiece.wRook;
                        break;
                    case "Q":
                        piece = board.blacktomove ? Board.ChessPiece.bQueen : Board.ChessPiece.wQueen;
                        break;
                    case "K":
                        piece = board.blacktomove ? Board.ChessPiece.bKing : Board.ChessPiece.wKing;
                        break;
                    default:
                        return false;
                }
                move_array[1] = board.convert(sq);

                for (int i = 0; i < 63; i++)
                {
                    if (board.canMove(i, move_array[1]) && board.hasPiece(i, piece))
                    {
                        move_array[0] = i;
                        return true;
                    }
                }
                return false;
            }
            if (rex4.IsMatch(move))
            {
                mv = rex4.Match(move);
                String sq_on = mv.Groups[1].Value + "8"; //Partial
                String sq = mv.Groups[2].Value + mv.Groups[3].Value;

                int file_on = board.fileof(board.convert(sq_on));
                int file = board.fileof(board.convert(sq));


                move_array[1] = board.convert(sq);

                for (int i = 0; i < 63; i++)
                {
                    bool on_file = file_on == board.fileof(i);
                    if (board.canMove(i, move_array[1]) && board.hasPiece(i,Board.ChessPiece.Pawn) && on_file)
                    {
                        move_array[0] = i; 
                        return true;
                    }
                }
                return false;
            }
            if (rex3.IsMatch(move))
            {
                mv = rex3.Match(move);
                String sq = mv.Groups[1].Value + mv.Groups[2].Value;

                move_array[1] = board.convert(sq);
                for (int i = 0; i < 63; i++)
                {
                    if (board.canMove(i, move_array[1]) && board.hasPiece(i,Board.ChessPiece.Pawn))
                    {
                        move_array[0] = i;
                        return true;
                    }
                }
                return false;
            }
            if (rex2.IsMatch(move))
            {
                if (board.blacktomove)
                {
                    move_array[0] = 60;
                    move_array[1] = 58;
                }
                else
                {
                    move_array[0] = 4;
                    move_array[1] = 2;
                }
                return true;
            }
            if (rex1.IsMatch(move))
            {
                if (board.blacktomove)
                {
                    move_array[0] = 60;
                    move_array[1] = 62;
                }
                else
                {
                    move_array[0] = 4;
                    move_array[1] = 6;
                }
                return true;
            }
            return false;
        }

        public static string convertMove(Board board,int[] move_array)
        {
            //TODO: Correct Problem (e.g Nc3 as N2c3)
            String move ="";
            if (move_array.Length != 2)
                return null;
            int square_on = move_array[0];
            int square = move_array[1];

            if (board.hasPiece(square_on, Board.ChessPiece.Pawn))
            {
                //TODO: Add case for  en-passant
                if (board.hasPiece(square)){ //Capture
                    move = board.convert(square_on).Substring(0, 1)+"x"+board.convert(square);
                }
                else //Move
                {
                    move = board.convert(square);
                }
                if (board.rankof(square) == 8)
                {
                    move += "=Q";
                }

                Board testboard = new Board();
                testboard.clonePosition(board);
                testboard.MovePiece(square_on,square);
                testboard.blacktomove = !testboard.blacktomove;
                if (testboard.IsCheckmate())
                    move += "#";
                else if (testboard.IsKingInCheck())
                    move += "+";

                return move;
            }

            if (board.hasPiece(square_on, Board.ChessPiece.King))
            {
                if ((square_on == 4 && square == 6) || (square_on == 60 && square == 62))
                    move = "O-O";
                if ((square_on == 4 && square == 2) || (square_on == 60 && square == 58))
                    move = "O-O-O";

                if (board.canCastle(square_on, square))
                {
                    Board testboard = new Board();
                    testboard.clonePosition(board);
                    testboard.MovePiece(square_on, square);
                    testboard.blacktomove = !testboard.blacktomove;
                    if (testboard.IsCheckmate())
                        move += "#";
                    else if (testboard.IsKingInCheck())
                        move += "+";
                    return move;
                }
            }

            if (board.hasPiece(square_on))
            {
                Board.ChessPiece piece = board.squares[square_on];
                String p = "";
                String sq_on = "";
                String sq = board.convert(square);
                switch (piece)
                {
                    case Board.ChessPiece.wBishop:
                    case Board.ChessPiece.bBishop:
                        p = "B";
                        break;
                    case Board.ChessPiece.wKnight:
                    case Board.ChessPiece.bKnight:
                        p = "N";
                        break;
                    case Board.ChessPiece.wRook:
                    case Board.ChessPiece.bRook:
                        p = "R";
                        break;
                    case Board.ChessPiece.wQueen:
                    case Board.ChessPiece.bQueen:
                        p = "Q";
                        break;
                    case Board.ChessPiece.wKing:
                    case Board.ChessPiece.bKing:
                        p = "K";
                        break;
                    default:
                        break;
                }

                bool rank_diff = false;
                bool file_diff = false;
                char f;
                char r;
                for (int i = 0; i < 63; i++)
                {   
                    if (board.canMove(i, square) && i!= square_on)
                    {
                        if(board.rankof(i) != board.rankof(square) &&board.hasPiece(i,piece))
                        {
                            rank_diff = true; //Two pieces of the same rank can capture on the square
                        }
                        if(board.fileof(i) != board.rankof(square) && board.hasPiece(i,piece))
                        {
                            file_diff = true; //Two pieces of the same file can capture on the square
                        }
                    }
                }

                if (!rank_diff && !file_diff)
                {
                    sq_on = "";
                }
                if (!rank_diff && file_diff)
                {
                    r = (char) (board.rankof(square_on) + '0');
                    sq_on = "0";
                    sq_on = sq_on.Replace('0', r);
                }
                if(rank_diff && !file_diff)
                {
                    f = (char)(board.fileof(square_on) + 'a' -1);
                    sq_on = "A";
                    sq_on = sq_on.Replace('A', f);
                }
                if(rank_diff && file_diff)
                {
                    r = (char)(board.rankof(square_on) + '0');
                    f = (char)(board.fileof(square_on) + 'a' - 1);
                    sq_on = "A0";
                    sq_on = sq_on.Replace('A', f);
                    sq_on = sq_on.Replace('0', r);
                }

                if (board.hasPiece(square)) //Capture
                    move = p + sq_on + "x"+ sq; 
                else
                    move = p + sq_on + sq;

                Board testboard = new Board();
                testboard.clonePosition(board);
                testboard.MovePiece(square_on, square);
                testboard.blacktomove = !board.blacktomove;
                if (testboard.IsCheckmate())
                    move += "#";
                else if (testboard.IsKingInCheck())
                    move += "+";

                return move;
            }
            return move;
        }
    }
}
