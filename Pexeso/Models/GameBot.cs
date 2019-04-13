using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class GameBot : Player
    {
        private GameBoard _board;
        private int?[,] memory;
        public GameBot(GameBoard board) : base()
        {
            _board = board;
            memory = new int?[board.Size, board.Size];
        }
    }
}
