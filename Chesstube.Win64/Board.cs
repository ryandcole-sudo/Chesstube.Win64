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
            NoPiece, //Square is empty
            wPawn,wKnight,wBishop,wRook,wQueen,wKing, // White Pieces
            bPawn, bKnight, bBishop, bRook, bQueen, bKing, //Black pieces
            Pawn,Knight,Bishop,Rook,Queen,King,
            MajorPiece,MinorPiece,BlackPiece,WhitePiece //Types

        };
   
        public Board()
        {
          for(int i = 0; i < 64; i++)
            {
                squares[i] = ChessPiece.NoPiece; //Fills the Board with empty squares
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
                squares[i] = -1; //Initialize all squares to -1
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

            if (square_on == square) //If the squares are the same then there is no point
            {
                return false;
            }


            if (!this.hasPiece(square_on)) //If there is no piece on the square, It can't control any square
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
                for (int i = sq_min+1; i < sq_max; i++)
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
                int direction = file_on - file; //Negative means to the right. Positive,to the left.

                int sq_min = Math.Min(square, square_on);
                int sq_max = Math.Max(square, square_on);


                //Checks to see if there are any pieces blocking the bishop

                for (int i = sq_min+1; i < sq_max-1; i++)
                {
                   int ir_diff = Math.Abs(rankof(square_on) - rankof(i)); //Rank difference between i aqnd sq_min
                   int if_diff = Math.Abs(fileof(square_on) - fileof(i)); //File difference beteween i and sq_min
                   int idir =     fileof(square_on) - fileof(i);
                   bool match_direction = (direction < 0  || idir > 0)&& (direction>0 || idir<0);
                   if(ir_diff == if_diff && match_direction)
                   {
                        Console.WriteLine(i);
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

                //Checks to see if there are pieces blocking the queen
                if (moves_as_bishop)
                {
                    int direction = file_on - file; //Negative means to the right. Positive,to the left.

                    for (int i = sq_min + 1; i < sq_max - 1; i++)
                    {
                        int ir_diff = Math.Abs(rankof(square_on) - rankof(i)); //Rank difference between i aqnd sq_min
                        int if_diff = Math.Abs(fileof(square_on) - fileof(i)); //File difference beteween i and sq_min
                        int idir = fileof(square_on) - fileof(i);
                        bool match_direction = (direction < 0 || idir > 0) && (direction > 0 || idir < 0);
                        if (ir_diff == if_diff && match_direction)
                        {
                            Console.WriteLine(i);
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
            }
            if (this.hasPiece(square_on, ChessPiece.King)) //If there is a king on the square
            {
                bool controls_it = (rank_diff == file_diff && rank_diff == 1) || (rank_diff + file_diff == 1);
                return controls_it;
            }
            if (this.hasPiece(square_on, ChessPiece.wPawn)) //If the square has a white pawn
            {
                bool controls_it = (rank_diff == 1 && file_diff == 1);
                controls_it = (controls_it) && (square_on < square)&&hasPiece(square);
                return controls_it;
            }
            if (this.hasPiece(square_on, ChessPiece.bPawn)) //If the square has a white pawn
            {
                bool controls_it = (rank_diff == 1 && file_diff == 1);
                controls_it = (controls_it) && (square_on > square)&&hasPiece(square);
                return controls_it;
            }

            return false; 

        }

        public int[] pieceControls(string square)
        {
            int sq = convert(square);
            return pieceControls(sq);
        }

        public bool pieceControls(string square_on, string square)
        {
            int sqo = convert(square_on);
            int sq = convert(square);
            return pieceControls(sqo, sq);
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

        public int[] pieceDefends(string square)
        {
            int sq = convert(square);
            return pieceDefends(sq);
        }

        public bool pieceDefends(string square_on, string square)
        {
            int sqo = convert(square_on);
            int sq = convert(square);

            return pieceDefends(sqo,sq);

        }

        public bool pieceAttacks(int square_on, int square) //Find out if the piece on [square_on] is defending the piece on [square]
        {
            bool same_color = pieceColor(square) == pieceColor(square_on); //Checks whether or not the pieces are the same color
            bool attacks_it = !same_color && hasPiece(square);

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

        public bool pieceAttacks(string square_on, string square)
        {
            int sqo = convert(square_on);
            int sq = convert(square);
            return pieceAttacks(sqo, sq);
        }

        public int[] pieceAttacks(string square)
        {
            int sq = convert(square);
            return pieceAttacks(sq);

        }

        public bool isAttacked(int square)
        {
            bool is_attacked = false;
            for(int i=0; i < 63; i++)
            {
                is_attacked = pieceAttacks(i, square) || is_attacked;
            }
            return is_attacked;
        }
        public bool isAttacked(string square)
        {
            return isAttacked(convert(square));
        }

        public bool canMove(int square_on, int square)
        {
            if (square > 63 || square_on >63) //Ignore 
            {
                return false;
            }
            ChessPiece piece = this.squares[square];
            ChessPiece piece_on = this.squares[square_on];

            bool same_color = pieceColor(square) == pieceColor(square_on); //Checks whether or not the pieces are the same color

            if(piece_on == ChessPiece.NoPiece) //Can't move if there is no piece to move 
            {
                return false;
            }

            if (same_color)
            {
                return false ; //False, since a piece can't capture another piece of the same color
            }
            if (canCastle(square, square_on))
                return true;

            bool blackmove_onwhite = pieceColor(square_on) == ChessPiece.BlackPiece && !blacktomove;
            bool whitemove_onblack = pieceColor(square_on) == ChessPiece.WhitePiece && blacktomove;
            if (blackmove_onwhite || whitemove_onblack)
                 return false;

            Board testboard = new Board();
            testboard.clonePosition(this);
            testboard.MovePiece(square_on, square);

            if (testboard.IsKingInCheck()) //If the king is still in check after the move, then it isn't valid.
                return false;
           
            if (pieceControls(square_on, square)) //If a piece controls a square, it can move to that square
            {
                return true;
            }

            if (hasPiece(square_on, ChessPiece.Pawn)) //Condition for pawn movement
            {
                int rank_on = rankof(square_on);
                int rank = rankof(square);

                int rank_f1 = rank_on + 1; //Rank one sqare forwards
                int rank_f2 = rank_on + 2; //Rank 2 squares forwards
                int rank_1 = 2; //Starting rank for the pawn

                if (hasPiece(square))
                {
                    return false; //Pawn can't move forwards to a square with another piece on it
                }

                if(fileof(square) != fileof(square_on))
                {
                    return false; //Pawn must stay on the same file, unless it is a capture (which is dealt with in another function)
                }

                if (hasPiece(square_on, ChessPiece.bPawn)) //Black Pawn moves the other way 
                {
                    rank_f1 = rank_on - 1;
                    rank_f2 = rank_on - 2;
                    rank_1 = 7;
                }

                if(rank == rank_f1)
                {
                    return true; //Pawns move forwards 1 square
                }
                if(rank_on == rank_1 && rank == rank_f2)
                {
                    return true; //Pawns can move 2 squares fowards on the first move
                }


                //TODO: Insert En passent rule

            }
   

            return false;
        }

        public bool canMove(string square)
        {
            return true; //TODO: Edit this
        }

        public bool canCastle(int square_on,int square)
        {
            if (IsKingInCheck())
                return false;
            bool can_castle = false;

            if (square_on == 4 && square == 6) //White King Side Castle
            {
                can_castle = white_king_castle && !hasPiece(5) && !hasPiece(6);
                can_castle = can_castle && !isAttacked(5) && !isAttacked(6);
                can_castle = can_castle && !blacktomove;
            }
            if (square_on == 4 && square == 2) //White Queen side castle
            {
                can_castle = white_queen_castle && !hasPiece(3) && !hasPiece(2) && !hasPiece(1);
                can_castle = can_castle && !isAttacked(3) && !isAttacked(2);
                can_castle = can_castle && !blacktomove;
            }
            if (square_on == 60 && square == 62) //Black King side castle
            {
                can_castle = black_king_castle && !hasPiece(61) && !hasPiece(62);
                can_castle = can_castle && !isAttacked(61) && !isAttacked(62);
                can_castle = can_castle && blacktomove;
            }
            if (square_on == 60 && square == 58) //Black Queen side castle
            {
                can_castle = black_queen_castle && !hasPiece(59) && !hasPiece(58) && !hasPiece(57);
                can_castle = can_castle && !isAttacked(59) && !isAttacked(58);
                can_castle = can_castle && blacktomove;
            }
            return can_castle;
        }

        public bool IsKingInCheck() //Determines whether or not the king  is in check.
        {
            int kng_sq = -1;
            for(int i=0;i<63;i++){
                if ( (!blacktomove && hasPiece(i, ChessPiece.wKing)) || (blacktomove && hasPiece(i, ChessPiece.bKing)))
                {
                    kng_sq = i;
                    break;
                }
            }
            if(kng_sq > 0)
            {
                return isAttacked(kng_sq);
            }

            return false;
        }


        public bool Move(int square_on, int square) //Moves a piece from one square to the next. Checks that the move is valid and makes the move if it is
        {
            en_passantsquare = 0;
            if (canCastle(square_on, square))
            {
                blacktomove = !blacktomove;
                MovePiece(square_on, square);
                if (square == 2) //white queen side
                    MovePiece(0, 3);
                if (square == 6)
                    MovePiece(7, 5); // white king side
                if (square == 58)
                    MovePiece(56, 59); //black queen side
                if (square == 62) //black king side
                    MovePiece(63, 61);
                return true;
            }
            if (canMove(square_on, square))
            {
                blacktomove = !blacktomove;
                MovePiece(square_on, square);

                if (hasPiece(square, ChessPiece.wKing)) //If the king moves, no more castling
                {
                    white_king_castle = false;
                    white_queen_castle = false;
                }
                if (hasPiece(square, ChessPiece.bKing))
                {
                    black_king_castle = false;
                    black_queen_castle = false;
                }
                if(hasPiece(square,ChessPiece.wRook))
                {
                    if (square_on == 0)
                        white_queen_castle = false;
                    if (square_on == 7)
                        white_king_castle = false;
                }
                if (hasPiece(square, ChessPiece.bRook))
                {
                    if (square_on == 56)
                        black_queen_castle = false;
                    if (square_on == 63)
                        black_king_castle = false;
                }

                if (rankof(square) == 8 && hasPiece(square, ChessPiece.wPawn)) //White Pawn Promotion
                    putPiece(square, ChessPiece.wQueen);
                if (rankof(square) == 1 && hasPiece(square, ChessPiece.bPawn)) //White Pawn Promotion
                    putPiece(square, ChessPiece.bQueen);
                return true;
            }
            
           
            return false;
        }

        public void Move(string square)
        {
             /*
              *  Move formats
              *  e4 Pawn forwards
              *  Ne4 Move piece
              *  Nxe4 Capture piece
              *  Nce4 Move piece (w/o amb)
              *  Ncxe4 Capture (w/o amb)
              *  exd5 Pawn capture
              *  0-0 Castle
              *  0-0-0 King side castle
              */
            //TODO: Add function body
        }
        public void Move(string square, string square_to)
        {
            int sq = convert(square);
            int sqt = convert(square_to);

            Move(sq, sqt);
        }

        public void MovePiece(int square_on, int square) //Moves a piece from one square to another without verifying the move
        {
            if(square_on == square)
                return;

            this.squares[square] = this.squares[square_on];
            this.squares[square_on] = ChessPiece.NoPiece;
        }
        
        public bool setup_fen(string fen_string) //Returns true if setup string is valid
        {
            squares = PGNReader.decode_FEN(fen_string);
            blacktomove = PGNReader.FEN_black_to_move(fen_string);

            bool[] array_castle = new bool[4];
            PGNReader.FEN_castle(fen_string,array_castle); 
            white_king_castle = array_castle[0];
            white_queen_castle = array_castle[1];
            black_king_castle = array_castle[2];
            black_queen_castle = array_castle[3];
            return false;
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
                        has_it = (the_piece == ChessPiece.wPawn || the_piece == ChessPiece.wRook || the_piece == ChessPiece.wKnight || the_piece == ChessPiece.wKing || the_piece == ChessPiece.wBishop || the_piece == ChessPiece.wQueen);
                        return has_it;
                    default:
                        return false; 

                }
                
                   
      
        }


        public ChessPiece pieceColor(int square) //Tells the color of the piece on a square 
        { 

            if (this.hasPiece(square, ChessPiece.WhitePiece))
            {
                return ChessPiece.WhitePiece; //The piece is white
            }
            else
            if (this.hasPiece(square, ChessPiece.BlackPiece))
            {
                return ChessPiece.BlackPiece; //The piece is black
            }
            else
            {
                return ChessPiece.NoPiece; //There is no piece on the square
            }
        }

        public void clonePosition(Board board) //Copies the completeposition from another board
        {
            whiteatop = board.whiteatop;
            blacktomove = board.blacktomove;
            en_passantsquare = board.en_passantsquare;
            white_king_castle = board.white_king_castle;
            black_king_castle = board.black_king_castle;
            white_queen_castle = board.white_queen_castle;
            black_queen_castle = board.black_queen_castle;
            halfmove_count = board.halfmove_count;
            fullmove_count = board.fullmove_count;

            for (int i = 0; i < 63; i++)
                squares[i] = board.squares[i]; 
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
            coord =  coord.Replace('A', file_letter);
            coord  = coord.Replace('5', rank_number);
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