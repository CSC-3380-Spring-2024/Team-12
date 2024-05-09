namespace Chesslogic
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public bool IsWithinBoardBounds()
        {
            return Row >= 0 && Row < 8 && Column >= 0 && Column < 8;
        }

        
        public bool IsValid()
        {
            return IsWithinBoardBounds();
        }
        public static bool IsValid(int row, int column)
    {
        return row >= 0 && row < 8 && column >= 0 && column < 8;
    }

        public static Position operator +(Position position, PositionDirection direction) 
        {
            return new Position(position.Row + direction.RowChange, position.Column + direction.ColumnChange);
        }
        public static Position operator +(Position a, Position b) {
    return new Position(a.Row + b.Row, a.Column + b.Column);
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
