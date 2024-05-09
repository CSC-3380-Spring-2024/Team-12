

namespace Chesslogic
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen;
        public int Row { get; }
        public int Column { get; }
        public override Player Color { get; }
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

        public Queen(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public Queen(int row, int col, Player color)
        {
            this.row = row;
            this.col = col;
            Color = color;
        }
        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return MovePositionInDir(from, board, dirs).Select(to => new NormalMove(from, to));
        }
         public override bool CanMoveTo(Position position, Board board)
    {
        int rowDiff = Math.Abs(position.Row - this.Row);
        int colDiff = Math.Abs(position.Column - this.Column);
        if (rowDiff == colDiff || position.Row == this.Row || position.Column == this.Column)
        {
            return IsPathClear(this.Row, this.Column, position.Row, position.Column, board);
        }
        return false;
    }

    private bool IsPathClear(int startRow, int startCol, int endRow, int endCol, Board board)
    {
        int rowStep = Math.Sign(endRow - startRow);
        int colStep = Math.Sign(endCol - startCol);
        int distance = Math.Max(Math.Abs(endRow - startRow), Math.Abs(endCol - startCol));

        for (int step = 1; step < distance; step++)
        {
            int intermediateRow = startRow + step * rowStep;
            int intermediateCol = startCol + step * colStep;
            if (board[new Position(intermediateRow, intermediateCol)] != null)
                return false;
        }

return board[new Position(endRow, endCol)].Color != this.Color;
    }
    }
}
