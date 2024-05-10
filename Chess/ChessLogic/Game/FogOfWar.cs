using Chesslogic;

namespace Chesslogic
{
    public class FogOfWarChessGame : GameDecorator 
    {
        private bool[,] visibilityGrid;

         public FogOfWarChessGame(Game wrappedGame) : base(wrappedGame)
        {
            this.Board = wrappedGame.Board; 
            visibilityGrid = new bool[8, 8];
            UpdateVisibility();
        }

        public override void MakeMove(Moves move)
        {
  
            var piece = Board[move.From];
            Board[move.From] = null;
            Board[move.To] = piece;

      
            CurrentTurn = CurrentTurn.Opponent();

            UpdateVisibility();
        }

        public override IEnumerable<Moves> LegalMoves(Position pos)
        {
            var moves = new List<Moves>(); 
            if (Board[pos] != null)
            {
          
                moves.AddRange(Board[pos].GetMoves(pos, Board));
            }

            
            return moves.Where(move => IsVisible(move.From) && IsVisible(move.To));
        }

        private void UpdateVisibility()
        {
            
            Array.Clear(visibilityGrid, 0, visibilityGrid.Length);

           
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var pos = new Position(row, col);
                    if (Board[pos] != null && Board[pos].Color == CurrentTurn)
                    {
                        SetVisibilityAroundPosition(pos);
                    }
                }
            }
        }

        private void SetVisibilityAroundPosition(Position position)
        {

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = position.Row + i;
                    int newCol = position.Column + j;
                    if (Position.IsValid(newRow, newCol))
                    {
                        visibilityGrid[newRow, newCol] = true;
                    }
                }
            }
        }
        public bool IsInBounds(Position position)
        {
            return position.Row >= 0 && position.Row < 8 && position.Column >= 0 && position.Column < 8;
        }
        public override bool IsVisible(Position position)
{
    if (IsInBounds(position))
    {
        return visibilityGrid[position.Row, position.Column];
    }
    return false;
}
    }
}
