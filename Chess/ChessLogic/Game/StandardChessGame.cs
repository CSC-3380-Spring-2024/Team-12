using Chesslogic;

namespace Chesslogic
{
    public class StandardChessGame : Game
    {
        public StandardChessGame(Board board) : base(board)
        {
            CurrentTurn = Player.White;
        }

        public override IEnumerable<Moves> LegalMoves(Position pos)
        {
           
            return Board[pos]?.GetMoves(pos, Board) ?? Enumerable.Empty<Moves>();
        }

        public override void MakeMove(Moves move)
        {
           
            move.Execute(Board);
            CurrentTurn = CurrentTurn.Opponent();
        }
    }
}
