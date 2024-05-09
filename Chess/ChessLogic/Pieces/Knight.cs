

namespace Chesslogic
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;

        public override Player Color { get; }
        public int Row { get; }
        public int Column { get; }
        public Knight(Player color)
        {
            Color = color;

        }
        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public Knight(int row, int col, Player color)
        {
            this.row = row;
            this.Column = col;
            Color = color;
        }
        private static IEnumerable<Position> PotentialPositions(Position from)
        {
            foreach (PositionDirection dir in new PositionDirection[] {PositionDirection.Up, PositionDirection.Down})
            {
                foreach(PositionDirection dir2 in new PositionDirection[] {PositionDirection.Left, PositionDirection.Right})
                {
                    yield return from + 2 *dir + dir2;
                    yield return from + dir + 2 *dir2;
                }
            }
        }
        public override bool CanMoveTo(Position position, Board board)
    {
        int rowDiff = Math.Abs(position.Row - this.Row);
        int colDiff = Math.Abs(position.Column - this.Column);
        return ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
               && (board[position] == null || board[position].Color != this.Color);
    }
        private IEnumerable<Position> actualPositions(Position from, Board board)
        {
            return PotentialPositions(from).Where(pos => Board.IsInBounds(pos) && (board.isEmpty(pos)
            || board[pos].Color != Color));
        }
        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return actualPositions(from, board).Select(to => new NormalMove(from, to));
        }

    }
}
