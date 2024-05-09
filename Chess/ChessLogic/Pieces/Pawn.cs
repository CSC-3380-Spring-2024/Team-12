

namespace Chesslogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        public int Row { get; }
        public int Column { get; }
        public bool HasMoved { get; set; } = false;

        private readonly PositionDirection forward;

        public Pawn(Player color)
        {
            Color = color;
            if(Color == Player.White)
            {
                forward = PositionDirection.Up;
            }
            else
            {
                forward = PositionDirection.Down;
            }
        }
        private static bool canMove(Position pos, Board board)
        {
            return Board.IsInBounds(pos) && board[pos] == null;
        }
         public override bool CanMoveTo(Position position, Board board)
    {
        int forwardDirection = (Color == Player.White) ? 1 : -1;
        if (position.Column == this.Column) 
        {
            if (position.Row == this.Row + forwardDirection && board.isEmpty(position))
            {
                return true;
            }
            if (!HasMoved && position.Row == this.Row + 2 * forwardDirection && board.isEmpty(position))
            {
                return board.isEmpty(new Position(this.Row + forwardDirection, this.Column));
            }
        }
        else if (Math.Abs(position.Column - this.Column) == 1 && position.Row == this.Row + forwardDirection) 
        {
            return !board.isEmpty(position) && board[position].Color != this.Color;
        }
        return false;
    }
        private bool CanCapture(Position pos, Board board)
        {
            if(!Board.IsInBounds(pos) || board.isEmpty(pos))
            {
                return false;
            }
            return board[pos].Color != Color;
        }

 private IEnumerable<Moves> ForwardMoves(Position from, Board board)
{
    int promotionRow = (Color == Player.White) ? 7 : 0; 
    Position oneStepForward = from + forward;
    
    
    if (oneStepForward.Row == promotionRow && canMove(oneStepForward, board))
    {
        yield return new PromotionMove(from, oneStepForward, this.Color);  
    }
    else if (canMove(oneStepForward, board))
    {
        yield return new NormalMove(from, oneStepForward);
    }

  
    if (!HasMoved)
    {
        Position twoStepsForward = oneStepForward + forward;
        if (canMove(oneStepForward, board) && canMove(twoStepsForward, board))
        {
            yield return new NormalMove(from, twoStepsForward);
        }
    }
}
public class PromotionMove : Moves
{
    public Player Color { get; private set; }  

    public PromotionMove(Position from, Position to, Player color) : base(from, to)
        {
            Color = color; 
        }
        public override MovementType Type => MovementType.Promotion;  
    

     public override void Execute(Board board)
        {
           
            Piece piece = board[From];
            if (piece != null)
            {
                
                board[To] = new Queen(Color);
                board[From] = null;  
                piece.HasMoved = true;  
            }
        }
}

private IEnumerable<Moves> Captures(Position from, Board board)
{
    int promotionRow = (Color == Player.White) ? 7 : 0;
    foreach (PositionDirection dir in (Color == Player.White) ? new[] {PositionDirection.UpLeft, PositionDirection.UpRight} : new[] {PositionDirection.DownLeft, PositionDirection.DownRight})
    {
        Position targetPos = from + dir;
        if (CanCapture(targetPos, board))
        {
            if (targetPos.Row == promotionRow)
            {
                yield return new PromotionMove(from, targetPos, this.Color);
            }
            else
            {
                yield return new NormalMove(from, targetPos);
            }
        }
    }
}


        public override IEnumerable<Moves> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(Captures(from, board));
        }
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public Pawn(int row, int col, Player color)
        {
            this.row = row;
            this.Column = col;
            Color = color;
        }

    }
}
