using Chesslogic;

namespace Chesslogic{
public class HalfChessGame : Game
{
    public HalfChessGame(Board board) : base(board)
    {
        SetupHalfBoard();
    }

    private void SetupHalfBoard()
    {
        Board.Clear();

        Board[0, 0] = new Rook(Player.Black);
        Board[0, 1] = new Knight(Player.Black);
        Board[0, 2] = new Bishop(Player.Black);
        Board[0, 3] = new Queen(Player.Black);

        Board[7, 4] = new King(Player.White);
        Board[7, 5] = new Bishop(Player.White);
        Board[7, 6] = new Knight(Player.White);
        Board[7, 7] = new Rook(Player.White);

        
        for (int i = 0; i < 4; i++)
        {
            Board[1, i] = new Pawn(Player.Black);
            Board[6, 4 + i] = new Pawn(Player.White);
        }
    }
    
}
}