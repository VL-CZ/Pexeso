using Pexeso.Enums;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class Game : ObservableObject
    {
        public GameBoard Board { get; }
        public Player Player { get; }
        public GameBot Bot { get; }
        private PlayerType _currentPlayer;
        /// <summary>
        /// get current player
        /// </summary>
        public PlayerType CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                RaisePropertyChanged();
            }
        }

        private string _winner;
        /// <summary>
        /// check if there's winner
        /// </summary>
        public string Winner
        {
            get => _winner;
            private set
            {
                _winner = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// size of the game board
        /// </summary>
        private readonly int _boardSize = 8;

        public Game()
        {
            Board = new GameBoard(_boardSize);
            Player = new Player();
            Bot = new GameBot(Board.BoxCount, Board);
            CurrentPlayer = PlayerType.Player;
        }

        /// <summary>
        /// execute moves of the player and bot
        /// </summary>
        /// <param name="cell1_ID"></param>
        /// <param name="cell2_ID"></param>
        public async Task ExecuteMove(int cell1_ID, int cell2_ID)
        {
            #region Player's move
            CurrentPlayer = PlayerType.Player;

            Box box1 = Board.GetBoxByID(cell1_ID);
            Box box2 = Board.GetBoxByID(cell2_ID);

            bool playersMatch = Player.ExecuteMove(box1, box2); // player's move

            Bot.AddToKnownBoxes(box1); // add cells to bot's memory
            Bot.AddToKnownBoxes(box2);
            CheckWinner();

            if (playersMatch) // match -> skip opponent's move
                return;

            #endregion
            #region Bot's move

            CurrentPlayer = PlayerType.Bot;
            bool opponentsMatch = false;
            do
            {
                if (Winner == null) // bot's move
                {
                    await Task.Delay(500);
                    opponentsMatch = await Bot.ExecuteMove();
                    CheckWinner();
                }
                else
                    break;
            } while (opponentsMatch == true); // repeat opponent's move while match

            #endregion

            CurrentPlayer = PlayerType.Player;
        }

        /// <summary>
        /// check if there's winner
        /// </summary>
        public void CheckWinner()
        {
            if (Player.Score + Bot.Score == Board.PairCount)
            {
                if (Player.Score > Bot.Score)
                {
                    Winner = nameof(Player);
                }
                else if (Player.Score < Bot.Score)
                {
                    Winner = nameof(Bot);
                }
                else
                {
                    Winner = "Draw";
                }
            }
        }
    }
}