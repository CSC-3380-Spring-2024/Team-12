namespace Chesslogic
{

    public abstract class Game
    {
        public Board Board { get; protected set; }
        public Player CurrentTurn { get; protected set; }
 private Stack<Moves> moveHistory = new Stack<Moves>();

        protected Game(Board board)
        {
            Board = board;
        }


        public virtual IEnumerable<Moves> LegalMoves(Position pos)
        {
            var moves = new List<Moves>();
            var piece = Board[pos];

            if (piece == null) return moves;  
            var possibleMoves = piece.GetMoves(pos, Board);

            foreach (var move in possibleMoves)
            {
                if (IsValidMove(move))
                {
                    moves.Add(move);
                }
            }

            return moves;
        }
        public virtual bool IsVisible(Position position)
        {
            return true; // By default, every position is visible unless specified otherwise by a subclass.
        }
        public virtual bool IsValidMove(Moves move)
        {
            
            if (!Board.IsInBounds(move.To))
            {
                return false;
            }

            
            MakeMove(move);  
            bool inCheckAfterMove = IsCheck(CurrentTurn);  
            UndoMove(move); 

            
            return !inCheckAfterMove;
        }

        protected bool WouldCauseCheck(Moves move)
        {
            
            MakeMove(move);
            bool causesCheck = IsCheck(CurrentTurn.Opponent()); 
            UndoMove(move);  

            return causesCheck;
        }
        public virtual void MakeMove(Moves move)
{
    
    Piece movingPiece = Board[move.From];
    Board[move.To] = movingPiece;
    Board[move.From] = null;

   
    moveHistory.Push(move);

    CurrentTurn = CurrentTurn.Opponent();  
}

        protected bool IsCheck(Player player)
{
    Position kingPosition = FindKingPosition(player);
    
    return IsPositionUnderAttack(kingPosition, player.Opponent());
}

private Position FindKingPosition(Player player)
{
    for (int row = 0; row < 8; row++)
    {
        for (int col = 0; col < 8; col++)
        {
            if (Board[row, col] != null && Board[row, col] is King && Board[row, col].Color == player)
            {
                return new Position(row, col);
            }
        }
    }
    throw new Exception("King not found, which should be impossible.");
}

protected bool IsPositionUnderAttack(Position position, Player byPlayer)
{
    
    return Board.EnumeratePositions()
        .Where(pos => Board[pos] != null && Board[pos].Color == byPlayer)
        .Any(pos => Board[pos].CanMoveTo(position, Board));
}
public void UndoMove(Moves move)
{
    if (moveHistory.Count > 0)
    {
        Moves lastMove = moveHistory.Pop(); 

        
        Piece movedPiece = Board[lastMove.To];
        Board[lastMove.From] = movedPiece;  
        Board[lastMove.To] = null;  

        

        CurrentTurn = CurrentTurn.Opponent();  
    }
}



    }
}
