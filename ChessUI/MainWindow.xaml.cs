﻿using Chesslogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] PieceImages = new Image[8, 8];

        private readonly Game gameState;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            gameState = new Game(Player.White, Board.Initialize());
            Showcase(gameState.Board);
        }
        private void InitializeBoard()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Image image = new Image();
                    PieceImages[x, y] = image;
                    PieceGrid.Children.Add(image);

                }
            }
        }
        private void Showcase(Board board)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = board[x, y];
                    PieceImages[x, y].Source = Images.GetImage(piece);
                }
            }
        }
<<<<<<< HEAD
=======
        private void BoardGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsMenuOnScreen())
            {
                return;
            }

            System.Windows.Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquare(point);

            if (selectedPosition == null)
            {
                OnFromSelected(pos);
            }
            else
            {
                onToSelected(pos);
            }
        }
        private Position ToSquare(System.Windows.Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }
        private void OnFromSelected(Position pos)
        {
            IEnumerable<Moves> moves = gameState.LegalMoves(pos);

            if (moves.Any())
            {
                selectedPosition = pos;
                cacheMoves(moves);
                ShowHighlights();
            }
        }
        private void onToSelected(Position pos)
        {
            selectedPosition = null;
            HideHighlights();

            if (moveCache.TryGetValue(pos, out Moves move))
            {
                if (move.Type == MovementType.Promotion)
                {
                    HandlePromotion(move.FromPosition, move.ToPosition);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }
        private void HandlePromotion(Position fromPosition, Position toPosition)
        {
            PieceImages[toPosition.Row, toPosition.Column].Source = Images.GetImage(gameState.Current, PieceType.Pawn);
            PieceImages[fromPosition.Row, fromPosition.Column].Source = null;

            PromotionMenu promoMenu = new PromotionMenu(gameState.Current);
            MenuContainer.Content = promoMenu;

            promoMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Moves promomove = new Promotion(fromPosition, toPosition, type);
                HandleMove(promomove);
            };
        }
        private void HandleMove(Moves move)
        {
            gameState.MakeMove(move);
            Showcase(gameState.Board);
            System.Diagnostics.Debug.WriteLine(gameState.IsGameOver());
            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }

        private void cacheMoves(IEnumerable<Moves> move)
        {
            moveCache.Clear();
            foreach (Moves m in move)
            {
                moveCache[m.ToPosition] = m;
            }
        }
        private void ShowHighlights()
        {
            System.Windows.Media.Color color = Color.FromArgb(100, 0, 255, 0);
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }
        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(Colors.Transparent);
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        private void RestartGame()
        {
            HideHighlights();
            moveCache.Clear();
            gameState = new Game(Player.White, Board.Initialize());
            Showcase(gameState.Board);
        }
>>>>>>> e4ac12f (FIXED CHECKFORGAMEOVER FUNCTION AAHHH)
    }
}