using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pexeso.Models
{
    class GameBoard : ObservableObject
    {
        public ObservableCollection<ObservableCollection<Box>> Board { get; }

        public int Size { get; }

        public int BoxCount
        {
            get
            {
                return Size * Size;
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
                Board = new ObservableCollection<ObservableCollection<Box>>();
                Size = N;
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

            for (int i = 0; i < Size; i++)
            {
                var row = new ObservableCollection<Box>();
                for (int j = 0; j < Size; j++)
                {
                    int index = generator.Next(0, possibleNumbers.Count);
                    int cellID = i * Size + j;

                    row.Add(new Box(cellID, possibleNumbers[index]));

                    possibleNumbers.RemoveAt(index);
                }
                Board.Add(row);
            }
        }

        public Box GetBoxByID(int id)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Box box = Board[i][j];
                    if (box.ID == id)
                    {
                        return box;
                    }
                }
            }
            return null;
        }
    }
}
