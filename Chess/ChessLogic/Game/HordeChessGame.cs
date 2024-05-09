using Chesslogic;

namespace Chesslogic
{
    public class HordeChessGame : StandardChessGame
    {
        public HordeChessGame(Board board) : base(board) 
        {
            InitializeHordeBoard();
        }

        private void InitializeHordeBoard()
        {
           
            Board.Clear();

            
            for (int row = 5; row <= 7; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Board[new Position(row, col)] = new Pawn(Player.White);
                }
            }

          
            Board[new Position(4, 1)] = new Pawn(Player.White); // b4
            Board[new Position(4, 2)] = new Pawn(Player.White); // c4
            Board[new Position(4, 5)] = new Pawn(Player.White); // f4
            Board[new Position(4, 6)] = new Pawn(Player.White); // g4

           
            Board[new Position(0, 0)] = new Rook(Player.Black);
            Board[new Position(0, 1)] = new Knight(Player.Black);
            Board[new Position(0, 2)] = new Bishop(Player.Black);
            Board[new Position(0, 3)] = new Queen(Player.Black);
            Board[new Position(0, 4)] = new King(Player.Black);
            Board[new Position(0, 5)] = new Bishop(Player.Black);
            Board[new Position(0, 6)] = new Knight(Player.Black);
            Board[new Position(0, 7)] = new Rook(Player.Black);

            
            for (int i = 0; i < 8; i++)
            {
                Board[new Position(1, i)] = new Pawn(Player.Black);
            }
        }
    }
}
