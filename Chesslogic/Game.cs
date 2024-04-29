﻿

namespace Chesslogic
{
    public class Game
    {
        public Board Board { get; }
        public Player Current { get; private set; }

        public Game(Player player, Board board)
        {
            Current = player;
            Board = board;
        }

        public IEnumerable<Moves> LegalMoves(Position pos)
        {
            if (Board.isEmpty(pos) || Board[pos].Color != Current)
            {
                return Enumerable.Empty<Moves>();
            }
            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }
        public void MakeMove(Moves move)
        {
            move.Execute(Board);
            Current = Current.Oppponent();
<<<<<<< HEAD
=======
            CheckForGameOver();
            System.Diagnostics.Debug.WriteLine(Result);
        }
        public IEnumerable<Moves> AllPossibleMoves(Player player)
        {
            IEnumerable<Moves> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });
            return moveCandidates.Where(move => move.isLegal(Board));
        }
        public void CheckForGameOver()
        {
            if (!AllPossibleMoves(Current).Any())
            {
                if (Board.IsInCheck(Current))
                {
                    Result = Result.Win(Current.Oppponent());
                }
                else
                {
                    Result = Result.Draw(Endreason.Stalemate);
                }
            }
        }
        public bool IsGameOver()
        {
            return Result != null;
>>>>>>> e4ac12f (FIXED CHECKFORGAMEOVER FUNCTION AAHHH)
        }
    }
}
