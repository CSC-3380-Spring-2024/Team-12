using System;
namespace Chesslogic
{
    public class ChessGameController
    {
        public Game CurrentGame { get; private set; }
        private Position selectedPosition;
        private bool isPositionSelected;
        private List<Moves> potentialMovess;  
         private List<Game> gameModes = new List<Game>();
    private Board board;
    private List<Position> highlightedPositions;

            public event Action OnUpdateRequested;


         public void RequestUpdate()
    {
        OnUpdateRequested?.Invoke();
    }
        public ChessGameController(Game game)
        {
            CurrentGame = game;
            isPositionSelected = false;
            potentialMovess = new List<Moves>();
                    highlightedPositions = new List<Position>();
                    this.board = board;

        }
        public void AddGameMode(Game gameMode)
    {
        gameModes.Add(gameMode);
        
    }

     public void RemoveGameMode(Type gameModeType)
    {
        gameModes.RemoveAll(g => g.GetType() == gameModeType);
    }

    public void MakeMove(Moves move)
{
    foreach (var game in gameModes)
    {
        Game currentGame = game;
        while (currentGame != null)
        {
            currentGame.MakeMove(move); 

            if (currentGame is GameDecorator decorator)
            {
                currentGame = decorator.WrappedGame; 
            }
            else
            {
                break; 
            }
        }
    }
    UpdateComponentState(); 
}


    public IEnumerable<Moves> GetLegalMoves(Position pos)
    {
        IEnumerable<Moves> moves = null;
        foreach (var game in gameModes)
        {
            moves = game.LegalMoves(pos);
        }
        return moves;
    }

    private void UpdateComponentState()
    {
        OnUpdateRequested?.Invoke();
    }
public bool ContainsGameMode(Type gameModeType)
    {
        return gameModes.Any(g => g.GetType() == gameModeType);
    }
    public T GetGameOfType<T>() where T : Game
{
    return gameModes.OfType<T>().FirstOrDefault();
}

    public GameDecorator GetGameOfType(Type gameModeType)
{
    return gameModes.OfType<GameDecorator>().FirstOrDefault(g => g.GetType() == gameModeType);
}



       

        public void HandleClick(Position pos)
{
    if (!isPositionSelected)
    {
        SelectPiece(pos);
    }
    else
    {
        if (selectedPosition.Equals(pos))
        {
            DeselectPiece();
        }
        else if (potentialMovess.Any(move => move.To.Equals(pos))) 
        {
            Moves move = potentialMovess.First(m => m.To.Equals(pos)); 
            if (IsMovesLegal(move))
            {
                ExecuteMoves(move);
            }
            else
            {
                Console.WriteLine("Move not legal. King would be in check.");
            }
        }
        else
        {
            Console.WriteLine("Move invalid or not found.");
        }
    }
    
}

        private void SelectPiece(Position pos)
        {
            if (CurrentGame.Board[pos] != null && CurrentGame.Board[pos].Color == CurrentGame.CurrentTurn)
            {
                selectedPosition = pos;
                isPositionSelected = true;
                potentialMovess = CurrentGame.LegalMoves(selectedPosition).ToList();
                 var moves = CurrentGame.LegalMoves(selectedPosition);
            highlightedPositions = moves.Select(move => move.To).ToList();
            RequestUpdate(); 
                Console.WriteLine($"Piece selected at {pos.Row}, {pos.Column}");
            }
        }

        private void DeselectPiece()
        {
            isPositionSelected = false;
            potentialMovess.Clear();
             highlightedPositions.Clear(); 
        RequestUpdate();
            Console.WriteLine("Deselecting piece.");
        }

        private void ExecuteMoves(Moves move)
        {
                CurrentGame.MakeMove(move);  
            isPositionSelected = false;
            highlightedPositions.Clear();
            RequestUpdate(); 
            if (IsCheck(CurrentGame.CurrentTurn))
            {
                Console.WriteLine("Check!");

            }
             
        }

        private bool IsMovesLegal(Moves move)
{
    return IsValidMove(move);
}
private bool IsValidMove(Moves move)
{
    
    var piece = CurrentGame.Board[move.From];
    if (piece == null) return false;

    
    var possibleMoves = piece.GetMoves(move.From, CurrentGame.Board);
    return possibleMoves.Any(m => m.To.Equals(move.To));
}

        private bool IsCheck(Player player)
        {
            
            var kingPosition = FindKingPosition(player);
            return CurrentGame.Board.IsUnderAttack(kingPosition, player.Opponent());
        }

        private Position FindKingPosition(Player player)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Position pos = new Position(row, col);
                    Piece piece = CurrentGame.Board[pos];
                    if (piece != null && piece.Type == PieceType.King && piece.Color == player)
                    {
                        return pos;
                    }
                }
            }
            throw new InvalidOperationException("King not found on the board, which should never happen.");
        }

         public string GetSquareClass(int row, int col)
    {
        Position pos = new Position(row, col);
        if (highlightedPositions.Contains(pos))
        {
            return "highlighted-square";
        }
        return "";
    }
        
public string GetPieceImage(Piece piece)
{
    if (piece == null) 
        return "path_to_default_image"; 

    switch (piece.Type)
{
    case PieceType.Pawn:
        return piece.Color == Player.White ? "Assets/PawnW.png" : "Assets/PawnB.png";
    case PieceType.Rook:
        return piece.Color == Player.White ? "Assets/RookW.png" : "Assets/RookB.png";
    case PieceType.Knight:
        return piece.Color == Player.White ? "Assets/KnightW.png" : "Assets/KnightB.png";
    case PieceType.Bishop:
        return piece.Color == Player.White ? "Assets/BishopW.png" : "Assets/BishopB.png";
    case PieceType.Queen:
        return piece.Color == Player.White ? "Assets/QueenW.png" : "Assets/QueenB.png";
    case PieceType.King:
        return piece.Color == Player.White ? "Assets/KingW.png" : "Assets/KingB.png";
    default:
        return "Assets/Default.png";  
}

}
public bool IsVisible(Position position)
{
    return ((FogOfWarChessGame)CurrentGame).IsVisible(position);
}


    }
}
