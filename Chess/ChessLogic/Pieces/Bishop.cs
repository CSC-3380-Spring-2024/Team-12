namespace Chesslogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }
        public int Row { get; }
        public int Column { get; }

        private static readonly PositionDirection[] dirs = new PositionDirection[]
        {
            PositionDirection.UpLeft,
            PositionDirection.UpRight,
            PositionDirection.DownLeft,
            PositionDirection.DownRight
        };
        public Bishop(Player color)
        {
            Color = color;
        }
        public Bishop(int row, int col, Player color)
        {
            this.row = row;
            this.Column = col;
            Color = color;
        }
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return MovePositionInDir(from, board, dirs).Select(to => new NormalMove(from, to));
        }
         public override bool CanMoveTo(Position position, Board board)
    {
        int rowDiff = Math.Abs(position.Row - this.Row);
        int colDiff = Math.Abs(position.Column - this.Column);
        if (rowDiff == colDiff)
        {
            
            int rowStep = position.Row > this.Row ? 1 : -1;
            int colStep = position.Column > this.Column ? 1 : -1;
            int steps = rowDiff;
            for (int i = 1; i < steps; i++)
            {
                if (board[new Position(this.Row + i * rowStep, this.Column + i * colStep)] != null)
                    return false;
            }
            return board[position] == null || board[position].Color != this.Color;
        }
        return false;
    }
    }
}
