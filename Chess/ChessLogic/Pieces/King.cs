

namespace Chesslogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        public int Row { get; }
        public int Column { get; }
        public bool HasMoved { get; set; } = false;

        private static readonly PositionDirection[] dirs = new PositionDirection[]
        {
            PositionDirection.Up,
            PositionDirection.Down,
            PositionDirection.Right,
            PositionDirection.Left,
            PositionDirection.UpLeft,
            PositionDirection.UpRight,
            PositionDirection.DownLeft,
            PositionDirection.DownRight
        };

        public King(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public King(int row, int col, Player color)
        {
            this.row = row;
            this.Column = col;
            Color = color;
        }
       public override bool CanMoveTo(Position position, Board board)
{
    int rowDiff = Math.Abs(position.Row - this.Row);
    int colDiff = Math.Abs(position.Column - this.Column);
    bool withinMoveRange = rowDiff <= 1 && colDiff <= 1 && (rowDiff > 0 || colDiff > 0);

    if (!withinMoveRange)
        return false;


    Piece pieceAtPosition = board[position];
    return pieceAtPosition == null || pieceAtPosition.Color != this.Color;
}

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach(PositionDirection dir in dirs)
            {
                Position to = from + dir;
                if(!Board.IsInBounds(to))
                {
                    continue;
                }
                if (board[to].Color != Color || board.isEmpty(to))
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
{
    foreach(Position to in MovePositions(from, board))
    {
        yield return new NormalMove(from, to);  
    }
}
    }
}
