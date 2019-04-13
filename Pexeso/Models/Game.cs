using Pexeso.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class Game
    {
        private GameBoard _board;
        private Player _player;
        private GameBot _bot;
        private PlayerType _nextPlayer;
        public Player Winner { get; private set; }
        public Game()
        {
            _board = new GameBoard(8);
            _player = new Player();
            _bot = new GameBot();
            _nextPlayer = PlayerType.Human;
        }

        public void Play()
        {
            while (_player.Score + _bot.Score < _board.PairCount)
            {
                SwitchPlayers();
            }
        }

        private void SwitchPlayers()
        {
            if (_nextPlayer == PlayerType.Human)
            {
                _nextPlayer = PlayerType.Bot;
            }
            else
                _nextPlayer = PlayerType.Human;
        }
    }
}
