namespace Chesslogic
{
    public abstract class Moves
    {
        public abstract MovementType Type { get; }
        public Position From { get; set; }
        public Position To { get; set; }
        public Moves(Position from, Position to)
        {
            From = from;
            To = to;
        }
        public abstract void Execute(Board board);
    }

   

}
