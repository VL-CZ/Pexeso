using Pexeso.Enums;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class Game : ObservableObject
    {
        public GameBoard Board { get; }
        public Player Player { get; }
        public GameBot Bot { get; }

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
        /// is the last move pair?
        /// </summary>
        public bool IsLastMovePair { get; private set; }

        /// <summary>
        /// size of the game board
        /// </summary>
        private readonly int _boardSize = 4;

        public Game()
        {
            Board = new GameBoard(_boardSize);
            Player = new Player();
            Bot = new GameBot(Board);
        }

        /// <summary>
        /// execute moves of the player and bot
        /// </summary>
        /// <param name="cell1_ID"></param>
        /// <param name="cell2_ID"></param>
        public void ExecuteMove(int cell1_ID, int cell2_ID)
        {
            Box box1 = Board.GetBoxByID(cell1_ID);
            Box box2 = Board.GetBoxByID(cell2_ID);
            IsLastMovePair = Player.ExecuteMove(box1, box2);



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