using System;
using System.Collections.Generic;
using System.Text;

namespace Chesstube
{
    class Board
    {
        public ChessPiece[] squares = new ChessPiece[64]; //Stores what piece are on what squares
        public bool blacktomove = false; //Is it Black's turn to move? If not it's White's turn

        //En passant square
        public int en_passantsquare =0;

        //Castling flags
        public bool white_king_castle = false; //Can White castle king side?
        public bool black_king_castle = false; //Can Black castle king side?
        public bool white_queen_castle = false; //Can White castle queen side?
        public bool black_queen_castle = false; //Can Black castle queen side?

        //Move counters
        public int halfmove_count = 0;
        public int fullmove_count = 0;

        //Board Orientation
        public bool whiteatop = false; 

        public enum ChessPiece
        {
            wPawn,wKnight,wBishop,wRook,wQueen,wKing, // White Pieces
            bPawn, bKnight, bBishop, bRook, bQueen, bKing, //Black pieces
            Pawn,Knight,Bishop,Rook,Queen,King,
            MajorPiece,MinorPiece,BlackPiece,WhitePiece, //Types
            NoPiece  //square is empty

        };
   
        public Board()
        {
          for(int i = 0; i < 64; i++)
            {
                squares[i] = ChessPiece.NoPiece; //Fills the Board with empty sqaures
            } 
        }
        
        public void putPiece(int square,ChessPiece piece) //Places a piece on a square, Add ChessPiece.NoPiece to empty the square
        {
            this.squares[square] = piece;
        }
        public void putPiece(string square, ChessPiece piece)
        {
            this.squares[convert(square)] = piece;
        }
        public bool hasPiece(int square)  //Queries whether or not the square is empty
        {
            if (this.squares[square] == ChessPiece.NoPiece)
                return false;
            else
                return true;
        }

        public bool hasPiece(string square)
        {
            int coord = convert(square);
            return hasPiece(coord);
        }

        public bool hasPiece(int square,ChessPiece piece)
        {
            ChessPiece the_piece = this.squares[square];
            return this.isA(the_piece, piece);
        }

        public bool hasPiece(string square,ChessPiece piece)
        {
            int coord = convert(square);
            return hasPiece(coord, piece);
        }

        public bool hasPiece(ChessPiece piece) //Checks if a certain piece exists on the board
        {
            for(int i=0; i < 63; i++)
            {
                if (hasPiece(i, piece))
                    return true;
            }
            return false;
        }
        public int[] pieceControls(int square) //Finds out what squares the piece on [square] is attacking/defending
        {
            ChessPiece the_piece = this.squares[square];
            int[] squares = new int[64];

            for (int i = 0; i < 63; i++)
            {
                squares[i] = -1; //Initialize all sqaures to -1
            }

            int k = 0; //Counts the number of squares controlled
          
            for(int i = 0; i < 63; i++)
            {
                if (pieceControls(square, i))
                {
                    squares[k] = i;
                    k++;
                }
                
            }
            return squares;

        }
        public bool pieceControls(int square_on,int square)
        {

            int rank_on = rankof(square_on);
            int file_on = fileof(square_on);

            int rank = rankof(square);
            int file = fileof(square);

            int rank_diff = Math.Abs(rank_on - rank);
            int file_diff = Math.Abs(file_on - file);

            if(square_on == square) //If the squares are the same then there is no point
            {
                return false;
            }


            if (!this.hasPiece(square_on)) //If  there is no piece on the square, It can't control any square
            {
                return false;
            }

            if (this.hasPiece(square_on, ChessPiece.Knight)) //If the square has a Knight
            {
                bool controls_it = (rank_diff != 0) && (file_diff != 0) && (file_diff + rank_diff == 3); //Basically an L shape
                return controls_it;
            }

            if (this.hasPiece(square_on, ChessPiece.Rook)) //If the square has a Rook
            {
                bool controls_it = (rank_diff == 0 || file_diff == 0); //If there are no pieces between the rook
                int direction = file_diff - rank_diff; //Positive if they are on the same rank, negative on the same file

                int sq_min = Math.Min(square, square_on);
                int sq_max = Math.Max(square, square_on);

                //Checks to see if there are pieces blocking the rook
                for (int i = sq_min+1; i < sq_max-1; i++)
                {
                      if (direction > 0 && (rankof(square) == rankof(i)))
                        controls_it = (controls_it) && (!this.hasPiece(i));
                      if (direction < 0 && (fileof(square) == fileof(i)))
                        controls_it = (controls_it) && (!this.hasPiece(i));
                }
                return controls_it;
               
            }
            if (this.hasPiece(square_on, ChessPiece.Bishop)) //If there is a bishop on the square
            {
                bool controls_it = (rank_diff == file_diff) && (rank_diff != 0);
                int direction = rank_on - rank; //Negative means to the right. Positive,to the left.

                int sq_min = Math.Min(square, square_on);
                int sq_max = Math.Max(square, square_on);

                //Check's to see if there are any pieces blocking the bishop

                for (int i = sq_min+1; i < sq_max-1; i++)
                {
                   int ir_diff = Math.Abs(rankof(sq_min) - rankof(i)); //Rank difference between i aqnd sq_min
                   int if_diff = Math.Abs(fileof(sq_min) - fileof(i)); //File difference beteween i and sq_min

                   if(ir_diff == if_diff)
                   {
                        controls_it = controls_it && (!this.hasPiece(i));
                   }
                }
                return controls_it;

            }
            if (this.hasPiece(square_on, ChessPiece.Queen)) //If there is a queen on the square
            {
                bool controls_it = (rank_diff == file_diff) || (file_diff == 0 || rank_diff == 0);

                bool moves_as_bishop = (rank_diff == file_diff) && (rank_diff != 0);
                bool moves_as_rook = (file_diff == 0 || rank_diff == 0);

                int sq_min = Math.Min(square, square_on);
                int sq_max = Math.Max(square, square_on);


                if (moves_as_bishop)
                {
                    int direction = rank_on - rank;

                    for (int i = sq_min + 1; i < sq_max - 1; i++)
                    {
                        int ir_diff = Math.Abs(rankof(sq_min) - rankof(i)); //Rank difference between i aqnd sq_min
                        int if_diff = Math.Abs(fileof(sq_min) - fileof(i)); //File difference beteween i and sq_min

                        if (ir_diff == if_diff)
                        {
                            controls_it = controls_it && (!this.hasPiece(i));
                        }
                    }

                    return controls_it;
                }

                if (moves_as_rook)
                {
                    int direction = file_diff - rank_diff;

                    for (int i = sq_min+1; i < sq_max-1; i++)
                    {
                        if (direction > 0 && (rankof(square) == rankof(i)))
                            controls_it = (controls_it) && (!this.hasPiece(i));
                        if (direction < 0 && (fileof(square) == fileof(i)))
                            controls_it = (controls_it) && (!this.hasPiece(i));
                    }
                    return controls_it;
                }


                //TODO: Check to see if there are pieces blocking the queen
            }
            if (this.hasPiece(square_on, ChessPiece.King)) //If there is a king on the square
            {
                bool controls_it = (rank_diff == file_diff && rank_diff == 1) || (rank_diff + file_diff == 1);
                return controls_it;
            }
            if (this.hasPiece(square_on, ChessPiece.wPawn)) //If the square has a white pawn
            {
                bool controls_it = (rank_diff == 1 && file_diff == 1);
                controls_it = (controls_it) && (square_on < square);
                return controls_it;
            }
            if (this.hasPiece(square_on, ChessPiece.bPawn)) //If the square has a white pawn
            {
                bool controls_it = (rank_diff == 1 && file_diff == 1);
                controls_it = (controls_it) && (square_on > square);
                return controls_it;
            }

            return false; 

        }
        public int[] pieceDefends(int square) //Finds out what pieces the piece on [square] is defending
        {
            ChessPiece the_piece = this.squares[square];
            int[] squares = new int[64];

            int k = 0; //Counts the number of squares controlled

            for (int i = 0; i < 63; i++)
            {
                if (pieceDefends(square, i))
                {
                    squares[k] = i;
                    k++;
                }

            }
            return squares;

        }
        public bool pieceDefends(int square_on,int square) //Find out if the piece on [square_on] is defending the piece on [square]
        {
            bool defends_it = (this.hasPiece(square, ChessPiece.WhitePiece) && this.hasPiece(square_on,ChessPiece.WhitePiece));
            defends_it = (this.hasPiece(square, ChessPiece.BlackPiece) && this.hasPiece(square_on, ChessPiece.BlackPiece)) && defends_it;

            defends_it = this.pieceControls(square_on, square) &&defends_it;

            return defends_it;
        }
        public bool pieceAttacks(int square_on, int square) //Find out if the piece on [square_on] is defending the piece on [square]
        {
            bool attacks_it = (this.hasPiece(square, ChessPiece.WhitePiece) && this.hasPiece(square_on, ChessPiece.BlackPiece));
            attacks_it = (this.hasPiece(square, ChessPiece.BlackPiece) && this.hasPiece(square_on, ChessPiece.WhitePiece)) && attacks_it;

            attacks_it = this.pieceControls(square_on, square) && attacks_it;

            return attacks_it;
        }

        public int[] pieceAttacks(int square) //Finds out what pieces the piece on [square] is defending
        {
            ChessPiece the_piece = this.squares[square];
            int[] squares = new int[64];

            int k = 0; //Counts the number of squares controlled

            for (int i = 0; i < 63; i++)
            {
                if (pieceAttacks(square, i))
                {
                    squares[k] = i;
                    k++;
                }

            }
            return squares;

        }


        public bool isA(ChessPiece the_piece, ChessPiece piece) //Is a piece the same class as another?
        {
            bool has_it = false;
            if (the_piece == piece)
                return true;
            else 
                switch (piece)
                {
                    case ChessPiece.Pawn:
                        return (the_piece == ChessPiece.wPawn) || (the_piece == ChessPiece.bPawn);
                    case ChessPiece.Knight:
                        return (the_piece == ChessPiece.wKnight) || (the_piece == ChessPiece.bKnight);
                    case ChessPiece.Bishop:
                        return (the_piece == ChessPiece.wBishop) || (the_piece == ChessPiece.bBishop);
                    case ChessPiece.Rook:
                        return (the_piece == ChessPiece.wRook) || (the_piece == ChessPiece.bRook);
                    case ChessPiece.Queen:
                        return (the_piece == ChessPiece.wQueen) || (the_piece == ChessPiece.bQueen);
                    case ChessPiece.King:
                        return (the_piece == ChessPiece.wKing) || (the_piece == ChessPiece.bKing);


                    case ChessPiece.MajorPiece:
                        has_it = (the_piece == ChessPiece.bRook || the_piece == ChessPiece.bQueen) || has_it;
                        has_it = (the_piece == ChessPiece.wRook || the_piece == ChessPiece.bQueen) || has_it;
                        return has_it;
                    case ChessPiece.MinorPiece:
                        has_it = (the_piece == ChessPiece.bBishop || the_piece == ChessPiece.bKnight) || has_it;
                        has_it = (the_piece == ChessPiece.wBishop || the_piece == ChessPiece.bKnight) || has_it;
                        return has_it;
                    case ChessPiece.BlackPiece:
                        has_it = (the_piece == ChessPiece.bPawn || the_piece == ChessPiece.bRook || the_piece == ChessPiece.bKnight || the_piece == ChessPiece.bKing || the_piece == ChessPiece.bBishop || the_piece == ChessPiece.bQueen);
                        return has_it;
                    case ChessPiece.WhitePiece:
                        has_it = (the_piece == ChessPiece.wPawn || the_piece == ChessPiece.wRook || the_piece == ChessPiece.wKnight || the_piece == ChessPiece.wKing || the_piece == ChessPiece.wBishop || the_piece == ChessPiece.bQueen);
                        return has_it;
                    default:
                        return false; 

                }
                
                   
      
        }

        public int convert(string square) 
        {
            char file_letter = square[0];
            char rank_number = square[1];

            int file = file_letter - 'a' + 1;
            int rank = rank_number - '0';

            int coord = 8 * (rank - 1) + file-1;
            

            return coord;
        }
        public string convert(int square)
        {
            int rank = square / 8+1; //There are 8 ranks per file   
            int file = square % 8 + 1; //There are 8 files in each rank

            char file_letter = (char)(file-1);
            file_letter += 'a';

            char rank_number = (char)(rank);
            rank_number += '0';

            string coord = "A5";
            coord.Replace('A', file_letter);
            coord.Replace('5', rank_number);
            return coord;
        }
        public int rankof(int square)
        { //Return the rank of the given square
            int rank = square / 8 + 1;
            return rank;
        }
        public int fileof(int square)
        {
            int file = square % 8 + 1;
            return file;
        }

        public int fileof(string square)
        {
            return fileof(convert(square));
        }
        public int rankof(string square)
        {
            return rankof(convert(square));
        }
    }
}