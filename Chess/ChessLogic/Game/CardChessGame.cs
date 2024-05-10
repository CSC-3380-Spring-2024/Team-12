using System;
using System.Collections.Generic;
using Chesslogic;

public class CardGame : Game
{
    private Dictionary<Player, CardCounts> cardCounts;
    private bool gameOver;
    private ChessSpaceStatus[,] chessSpaceStatuses;
    private Dictionary<Player, bool> hasPlayedCard;
    public Player CurrentPlayer { get; private set; }
    public Dictionary<Player, CardCounts> CardCounts => cardCounts;


    public CardGame(Board board) : base(board)
{
    InitializeGame();
    
}

    private void InitializeGame()
    {
        cardCounts = new Dictionary<Player, CardCounts>
        {
            { Player.White, new CardCounts() },
            { Player.Black, new CardCounts() }
        };
        gameOver = false;
        CurrentTurn = Player.White;
        CurrentPlayer = Player.White;
        hasPlayedCard = new Dictionary<Player, bool>
        {
            { Player.White, false },
            { Player.Black, false }
        };
    }

    public void PlayCard(Player player, CardType cardType, Position position)
{
    if (!gameOver && CurrentTurn == player && !hasPlayedCard[player])
    {
        switch (cardType)
        {
            case CardType.Flame:
                ApplyFlameCard(player, position);
                break;
            case CardType.Freeze:
                ApplyFreezeCard(player, position);
                break;
            case CardType.Possess:
                ApplyPossessCard(player, position);
                break;
        }
        CurrentTurn = CurrentTurn.Opponent();
        CurrentPlayer = CurrentPlayer.Opponent();
        hasPlayedCard[CurrentPlayer] = false;
        GiveRandomCardToPlayer(CurrentPlayer);
    }
}


    public void PlayCard(CardType cardType)
    {
        PlayCard(CurrentPlayer, cardType, new Position(0, 0));
    }

    private void ApplyFlameCard(Player player, Position position)
    {
        CardCounts playerCardCounts = cardCounts[player];

        if (playerCardCounts.Flame > 0)
        {
            playerCardCounts.Flame--;
            ApplyFlameEffect(position);
        }
    }

    private void ApplyFreezeCard(Player player, Position position)
    {
        CardCounts playerCardCounts = cardCounts[player];

        if (playerCardCounts.Freeze > 0)
        {
            playerCardCounts.Freeze--;
            ApplyFreezeEffect(position);
        }
    }

    private void ApplyPossessCard(Player player, Position position)
    {
        CardCounts playerCardCounts = cardCounts[player];

        if (playerCardCounts.Possess > 0)
        {
            playerCardCounts.Possess--;
            ApplyPossessEffect(position);
        }
    }
     private void InitializeChessSpaceStatuses(int rowCount, int columnCount)
    {
        chessSpaceStatuses = new ChessSpaceStatus[rowCount, columnCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                chessSpaceStatuses[i, j] = new ChessSpaceStatus();
            }
        }
    }

    private void ApplyFlameEffect(Position position)
    {
        int row = position.Row;
        int col = position.Column;
        if (row >= 0 && row < position.Row && col >= 0 && col < position.Column)
        {
            chessSpaceStatuses[row, col].IsFlame = true;
            chessSpaceStatuses[row, col].TurnsLeft = 4;
        }
    }

    private void ApplyFreezeEffect(Position position)
    {
        int row = position.Row;
        int col = position.Column;
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                if (r >= 0 && r < position.Row && c >= 0 && c < position.Column)
                {
                    chessSpaceStatuses[r, c].IsFrozen = true;
                    chessSpaceStatuses[r, c].TurnsLeft = 2;
                }
            }
        }
    }

    private void ApplyPossessEffect(Position position)
    {
        int row = position.Row;
        int col = position.Column;
        if (row >= 0 && row < position.Row && col >= 0 && col < position.Column)
        {
            if (!Board.isEmpty(position) && Board[position].Color != CurrentTurn)
            {
                var piece = Board[position];
                if (piece.Type != PieceType.King && piece.Type != PieceType.Queen)
                {
                    chessSpaceStatuses[row, col].IsPossessed = true;
                }
            }
        }
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    

    public ChessSpaceStatus GetChessSpaceStatus(Position position)
    {
        int row = position.Row;
        int col = position.Column;
        if (row >= 0 && row < 8 && col >= 0 && col < 8)
        {
            return chessSpaceStatuses[row, col];
        }
        else
        {
            return null;
        }
    }

    public void GiveRandomCardToPlayer(Player player)
    {
        CardType randomCardType = (CardType)new Random().Next(0, 3);
        switch (randomCardType)
        {
            case CardType.Flame:
                cardCounts[player].Flame++;
                break;
            case CardType.Freeze:
                cardCounts[player].Freeze++;
                break;
            case CardType.Possess:
                cardCounts[player].Possess++;
                break;
        }
    }

}

public enum CardType
{
    Flame,
    Freeze,
    Possess
}

public class CardCounts
{
    public int Flame { get; set; } = 0;
    public int Freeze { get; set; } = 0;
    public int Possess { get; set; } = 0;
}

public class ChessSpaceStatus
{
    public bool IsFlame { get; set; }
    public bool IsFrozen { get; set; }
    public bool IsPossessed { get; set; }
    public int TurnsLeft { get; set; }
}
