namespace Chesslogic
{
    public abstract class GameDecorator : Game
    {
        protected Game WrappedGame;

        public GameDecorator(Game wrappedGame) : base(wrappedGame.Board)
        {
            WrappedGame = wrappedGame;
        }

        public override IEnumerable<Moves> LegalMoves(Position pos)
        {
            return WrappedGame.LegalMoves(pos);
        }

        public override void MakeMove(Moves move)
        {
            WrappedGame.MakeMove(move);
        }
    }
}
