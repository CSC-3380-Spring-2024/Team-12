
namespace Chesslogic
{
    public class Board
    {
        
        public readonly Piece[,] pieces = new Piece[8, 8];
        public bool debugMode { get; set; } = false;


        
        public Piece this[int row, int col]
        {
            get
            {
                if (debugMode) 
                {
                    Console.WriteLine($"Attempting to access row {row}, col {col}");
                }
                if (row < 0 || row >= 8 || col < 0 || col >= 8)
                {
                    throw new IndexOutOfRangeException($"Row or column is out of the board's bounds: Row {row}, Col {col}");
                }
                return pieces[row, col];
            }
            set
            {
                if (debugMode) 
                {
                    Console.WriteLine($"Setting value at row {row}, col {col}");
                }
                if (row < 0 || row >= 8 || col < 0 || col >= 8)
                {
                    throw new IndexOutOfRangeException($"Row or column is out of the board's bounds: Row {row}, Col {col}");
                }
                pieces[row, col] = value;
            }
        }


        
        public Piece this[Position pos]
        {
            get => this[pos.Row, pos.Column];
            set => this[pos.Row, pos.Column] = value;
        }
        public Piece GetPieceAt(int row, int column)
        {
            if (row >= 0 && row < 8 && column >= 0 && column < 8)
            {
                return pieces[row, column];
            }
            return null; 
        }


        
        public static Board Initialize()
        {
            var board = new Board();
            board.AddBasePieces();
            return board;
        }

        
        public virtual void AddBasePieces()
        {
            
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
            }

            
            this[0, 0] = new Rook(Player.Black);
            this[0, 7] = new Rook(Player.Black);
            this[7, 0] = new Rook(Player.White);
            this[7, 7] = new Rook(Player.White);

           
            this[0, 1] = new Knight(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[7, 1] = new Knight(Player.White);
            this[7, 6] = new Knight(Player.White);

            
            this[0, 2] = new Bishop(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[7, 2] = new Bishop(Player.White);
            this[7, 5] = new Bishop(Player.White);

            
            this[0, 3] = new Queen(Player.Black);
            this[7, 3] = new Queen(Player.White);

            
            this[0, 4] = new King(Player.Black);
            this[7, 4] = new King(Player.White);
        }
        public virtual void AddBasePieces(Player player)
        {
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
            }

            
            this[0, 0] = new Rook(Player.Black);
            this[0, 7] = new Rook(Player.Black);
            this[7, 0] = new Rook(Player.White);
            this[7, 7] = new Rook(Player.White);

           
            this[0, 1] = new Knight(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[7, 1] = new Knight(Player.White);
            this[7, 6] = new Knight(Player.White);

            
            this[0, 2] = new Bishop(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[7, 2] = new Bishop(Player.White);
            this[7, 5] = new Bishop(Player.White);

            
            this[0, 3] = new Queen(Player.Black);
            this[7, 3] = new Queen(Player.White);

            
            this[0, 4] = new King(Player.Black);
            this[7, 4] = new King(Player.White);
        }

        
        public static bool IsInBounds(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }
        public bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }




        public bool isEmpty(Position pos)
        {
            return this[pos] == null;
        }
        public static Board InitializeStandard()
        {
            Board board = new Board();
            board.AddBasePieces();
            return board;
        }
        public bool IsUnderAttack(Position position, Player byPlayer)
        {

            int pawnRowDirection = byPlayer == Player.White ? -1 : 1;
            Position[] pawnAttacks = {
        new Position(position.Row + pawnRowDirection, position.Column + 1),
        new Position(position.Row + pawnRowDirection, position.Column - 1)
    };

            foreach (var pos in pawnAttacks)
            {
                if (IsInBounds(pos) && this[pos] is Pawn && this[pos].Color == byPlayer)
                    return true;
            }


            Position[] knightMoves = {
        new Position(2, 1), new Position(2, -1), new Position(-2, 1), new Position(-2, -1),
        new Position(1, 2), new Position(1, -2), new Position(-1, 2), new Position(-1, -2)
    };
            PositionDirection[] lineMoves = {
        new PositionDirection(1, 0), new PositionDirection(-1, 0),
        new PositionDirection(0, 1), new PositionDirection(0, -1)
    };
            PositionDirection[] diagonalMoves = {
        new PositionDirection(1, 1), new PositionDirection(1, -1),
        new PositionDirection(-1, 1), new PositionDirection(-1, -1)
    };


            foreach (var move in knightMoves)
            {
                Position pos = position + move;
                if (IsInBounds(pos) && this[pos] is Knight && this[pos].Color == byPlayer)
                    return true;
            }


            bool CheckLineOfAttack(PositionDirection direction)
            {
                Position current = position + direction;
                while (IsInBounds(current))
                {
                    if (this[current] != null)
                    {
                        if (this[current].Color == byPlayer &&
                            (this[current] is Queen ||
                             this[current] is Rook && (direction.RowChange == 0 || direction.ColumnChange == 0) ||
                             this[current] is Bishop && (direction.RowChange != 0 && direction.ColumnChange != 0)))
                        {
                            return true;
                        }
                        break;
                    }
                    current = current + direction;
                }
                return false;
            }


            foreach (var dir in lineMoves)
                if (CheckLineOfAttack(dir)) return true;

            foreach (var dir in diagonalMoves)
                if (CheckLineOfAttack(dir)) return true;


            foreach (var dir in lineMoves.Concat(diagonalMoves))
            {
                Position pos = position + dir;
                if (IsInBounds(pos) && this[pos] is King && this[pos].Color == byPlayer)
                    return true;
            }

            return false;
        }
        public void Clear()
        {
            for (int row = 0; row < pieces.GetLength(0); row++)
            {
                for (int col = 0; col < pieces.GetLength(1); col++)
                {
                    pieces[row, col] = null;
                }
            }
        }
        public IEnumerable<Position> EnumeratePositions()
        {

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    yield return new Position(row, col);
        }

    }
}
