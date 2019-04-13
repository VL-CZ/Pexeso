using Pexeso.Enums;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class Game : ObservableObject
    {
        private PlayerType _currentPlayerType;
        private Player _currentPlayer;
        public GameBoard Board { get; }
        public Player Player { get; }
        public GameBot Bot { get; }
        public string Winner { get; private set; }
        public bool IsLastMovePair { get; private set; }

        public Game()
        {
            Board = new GameBoard(8);
            Player = new Player();
            Bot = new GameBot(Board);
            _currentPlayer = Player;
            _currentPlayerType = PlayerType.Human;
        }

        public void ExecuteMove(int cell1_ID, int cell2_ID)
        {
            Box box1 = Board.GetBoxByID(cell1_ID);
            Box box2 = Board.GetBoxByID(cell2_ID);
            IsLastMovePair = Player.ExecuteMove(box1, box2);
        }

        private void SwitchPlayers()
        {
            if (_currentPlayerType == PlayerType.Human)
            {
                _currentPlayerType = PlayerType.Bot;
                _currentPlayer = Bot;
            }
            else
            {
                _currentPlayerType = PlayerType.Human;
                _currentPlayer = Player;
            }
        }
    }
}