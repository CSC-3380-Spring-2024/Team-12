namespace Chesslogic
{
    public class HordeBoard : Board
    {
        public static new HordeBoard Initialize()
        {
            HordeBoard board = new HordeBoard();
            board.AddBasePieces();
            return board;
        }

        public static new HordeBoard InitializeHordeMode()
        {
            HordeBoard board = new HordeBoard();
            board.AddBasePieces();
        
            board.AddExtraPawns(); 
            return board;
        }
         public virtual void Clear()
    {
        for (int row = 0; row < pieces.GetLength(0); row++)
        {
            for (int col = 0; col < pieces.GetLength(1); col++)
            {
                pieces[row, col] = null;
            }
        }
    }

        public Piece this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= 10 || col < 0 || col >= 8)
                    throw new IndexOutOfRangeException("Row or column is out of the board's bounds.");
                return pieces[row, col];
            }
            set
            {
                if (row < 0 || row >= 10 || col < 0 || col >= 8)
                    throw new IndexOutOfRangeException("Row or column is out of the board's bounds.");
                pieces[row, col] = value;
            }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

    

        public override void AddBasePieces()
        {
             this.Clear();
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
            }

            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

           
        for (int i = 0; i < 8; i++)
            {
                this[4, i] = new Pawn(Player.White);
                this[5, i] = new Pawn(Player.White);
            }
       
       
        }
       


        public void AddExtraPawns()
        {
            
            this[3, 1] = new Pawn(Player.White); // b4
            this[3, 2] = new Pawn(Player.White); // c4
            this[3, 5] = new Pawn(Player.White); // f4
            this[3, 6] = new Pawn(Player.White); // g4
        }

        public static bool IsInBounds(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 10 && pos.Column >= 0 && pos.Column < 8; 
        }

        public bool isEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
