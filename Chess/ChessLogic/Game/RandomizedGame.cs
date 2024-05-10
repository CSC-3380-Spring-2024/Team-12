using Chesslogic;

namespace Chesslogic{
public class RandomChessGame : Game
{
    public RandomChessGame(Board board) : base(board)
    {
        RandomizePieces();
    }

    private void RandomizePieces()
    {
        List<Piece> pieces = new List<Piece>
        {
            new Rook(Player.White), new Knight(Player.White), new Bishop(Player.White),
            new Queen(Player.White), new King(Player.White), new Bishop(Player.White),
            new Knight(Player.White), new Rook(Player.White),
            
        };

        List<int> positions = Enumerable.Range(0, 64).Where(i => i / 8 != 0 && i / 8 != 7).ToList();  
        Random rand = new Random();

        foreach (var piece in pieces)
        {
            int positionIndex = rand.Next(positions.Count);
            int position = positions[positionIndex];
            positions.RemoveAt(positionIndex);
            Board[position / 8, position % 8] = piece;
        }

        Board[0, 4] = new King(Player.White);
        Board[7, 4] = new King(Player.Black);
    }
}
}