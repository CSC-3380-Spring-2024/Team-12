using Chesslogic;

namespace Chesslogic
{
    public class AtomicChessGame : Game
    {
        private Game wrappedGame;

        public AtomicChessGame(Game wrappedGame, Board board) : base(board)
        {
            this.wrappedGame = wrappedGame ?? throw new ArgumentNullException(nameof(wrappedGame), "Wrapped game cannot be null.");
        }

        public override void MakeMove(Moves move)
        {
            Console.WriteLine($"Executing move from {move.From} to {move.To}");

         
            if (Board[move.To] != null && Board[move.To].Color != Board[move.From].Color)
            {
                Console.WriteLine("Capture move detected, triggering explosion.");
                TriggerAtomicExplosion(move.To);
            }
            else
            {
                Console.WriteLine("No capture, standard move execution.");
            }

            
            move.Execute(Board);

            
            CurrentTurn = CurrentTurn.Opponent();
            Console.WriteLine($"Move executed. It is now {CurrentTurn}'s turn.");
        }

        public override IEnumerable<Moves> LegalMoves(Position pos)
        {
            var moves = wrappedGame.LegalMoves(pos);
            var filteredMoves = new List<Moves>();

            foreach (var move in moves)
            {
                if (IsMoveAllowedInAtomicChess(move))
                {
                    filteredMoves.Add(move);
                }
            }

            return filteredMoves;
        }

        private bool IsMoveAllowedInAtomicChess(Moves move)
        {
            return true; 
        }

        private void TriggerAtomicExplosion(Position position)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = position.Row + i;
                    int newCol = position.Column + j;
                    if (Board.IsValidPosition(newRow, newCol))
                    {
                        var targetPosition = new Position(newRow, newCol);
                        if (Board[targetPosition] != null && Board[targetPosition].Type != PieceType.King)
                        {
                            Board[targetPosition] = null; 
                        }
                    }
                }
            }
            Console.WriteLine("Explosion triggered.");
        }
    }
}
