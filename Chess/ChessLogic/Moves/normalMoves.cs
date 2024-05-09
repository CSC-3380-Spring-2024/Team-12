namespace Chesslogic
{
    public class NormalMove : Moves
    {
        public override MovementType Type => MovementType.Move;
        public Position From { get; private set; }
        public Position To { get; private set; }

     
    public NormalMove(Position from, Position to) : base(from, to)
        {
            From = from;
            To = to;
        }

        public override void Execute(Board board)
        {
            Piece piece = board[From];
            board[To] = piece;
            board[From] = null;
            if (piece != null) piece.HasMoved = true;
        }
    }
}
