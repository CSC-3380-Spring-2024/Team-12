using Chesslogic;

namespace Chesslogic
{
    public class AllQueensChessGame : Game
    {
        public AllQueensChessGame(Board board) : base(board)
        {
            SetupAllQueensBoard();
        }

        private void SetupAllQueensBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (Board[row, col] is King) continue;

                    if (Board[row, col] != null)
                    {
                        Board[row, col] = new Queen(Board[row, col].Color);
                    }
                }
            }
        }
    }
}
