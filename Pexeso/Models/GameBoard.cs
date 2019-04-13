using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pexeso.Models
{
    class GameBoard
    {
        public Box[,] Board { get; }

        public int BoxCount
        {
            get
            {
                return Board.GetLength(0) * Board.GetLength(1);
            }
        }

        public int PairCount
        {
            get
            {
                return BoxCount / 2;
            }
        }

        /// <summary>
        /// N x N gameboard
        /// </summary>
        /// <param name="N"></param>
        public GameBoard(int N)
        {
            if (N % 2 == 1)
            {
                throw new ArgumentException();
            }
            else
            {
                Board = new Box[N, N];
                Initialize();
            }
        }

        private void Initialize()
        {
            List<int> possibleNumbers = new List<int>();
            Random generator = new Random();

            for (int i = 1; i <= PairCount; i++)
            {
                possibleNumbers.Add(i);
                possibleNumbers.Add(i);
            }

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    int index = generator.Next(0, possibleNumbers.Count);

                    Board[i, j] = new Box(possibleNumbers[index]);

                    possibleNumbers.RemoveAt(index);
                }
            }
        }

    }
}
