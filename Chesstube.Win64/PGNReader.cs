using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesstube
{
   class PGNReader
    {
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

        public bool FEN_castle(string fen_string,bool[] array_castle)
        {

            string[] fn_string = fen_string.Split(' ');

            string castle_string = fn_string[2];
            if(array_castle.Length < 3)
            {
                return false;
            }

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
            return false;
        }
        private static void permute_square(Board.ChessPiece[] square, Board.ChessPiece[] copy) //Performs a horizontal flip of the board squares
        {
            Board.ChessPiece[] cpy_sqr = new Board.ChessPiece[100];
            for(int i =0; i < 64; i++)
            {
                int rank = i/8 + 1; //consider:redundant
                int file = i%8 + 1;

                int j = (rank-1)*8 + 8 -file; //Find the complementary file
                copy[i] = square[j];

            }
           

        }
        
       
        

    }
}
