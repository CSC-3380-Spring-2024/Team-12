namespace Chesslogic
{
    public class CaptureMove : Moves
    {
        public override MovementType Type => MovementType.Capture;

        public CaptureMove(Position from, Position to) : base(from, to) { }

        public override void Execute(Board board)
        {
            Piece capturedPiece = board[To]; 
            Piece movingPiece = board[From];
            board[To] = movingPiece; 
            board[From] = null; 

            if (movingPiece != null)
                movingPiece.HasMoved = true;

       
        }
    }
}
