namespace Chesslogic
{
    public struct Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Position operator +(Position position, PositionDirection direction)
        {
            return new Position(position.Row + direction.RowChange, position.Column + direction.ColumnChange);
        }

        public Player SquareColor()
        {
            return (Row + Column) % 2 == 0 ? Player.White : Player.Black;
        }

        public override bool Equals(object obj)
        {
            if (obj is Position position)
            {
                return Row == position.Row && Column == position.Column;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !left.Equals(right);
        }
    }
}
