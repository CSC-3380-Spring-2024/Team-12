

namespace Chesslogic
{
    public class Rook : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public int Row { get; }
        public int Column { get; }
        public override Player Color { get; }

        public static readonly PositionDirection[] dirs = new PositionDirection[]
        {
            PositionDirection.Up,
            PositionDirection.Down,
            PositionDirection.Right,
            PositionDirection.Left
        };
        public bool HasMoved { get; set; } = false;
        public Rook(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public Rook(int row, int col, Player color)
        {
            this.row = row;
            this.Column = col;
            Color = color;
        }

        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return MovePositionInDir(from, board, dirs).Select(to => new NormalMove(from, to));
        }
        protected bool IsPathClear(int startRow, int startColumn, int endRow, int endColumn, Board board)
{
    int rowIncrement = (endRow > startRow) ? 1 : (endRow < startRow) ? -1 : 0;
    int columnIncrement = (endColumn > startColumn) ? 1 : (endColumn < startColumn) ? -1 : 0;

    int currentRow = startRow + rowIncrement;
    int currentColumn = startColumn + columnIncrement;

    while (currentRow != endRow || currentColumn != endColumn)
    {
        if (board.GetPieceAt(currentRow, currentColumn) != null)
        {
            return false; 
        }
        currentRow += rowIncrement;
        currentColumn += columnIncrement;
    }
    return true; 
}
         public override bool CanMoveTo(Position position, Board board)
    {
        if (position.Row == this.Row || position.Column == this.Column)
        {
            return IsPathClear(this.Row, this.Column, position.Row, position.Column, board);
        }
        return false;
    }
    }
}
