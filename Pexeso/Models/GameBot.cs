using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class GameBot : Player
    {
        private GameBoard _board;
        /// <summary>
        /// all knows values of boxes
        /// </summary>
        private List<Box> _knownBoxes;
        /// <summary>
        /// IDs of all boxes in game
        /// </summary>
        private List<int> _allBoxesIDs;
        private Random _generator;

        public GameBot(int boxesCount, GameBoard board) : base()
        {
            _generator = new Random();
            _knownBoxes = new List<Box>();
            _allBoxesIDs = new List<int>();
            _board = board;

            for (int i = 0; i < boxesCount; i++)
            {
                _allBoxesIDs.Add(i);
            }
        }

        /// <summary>
        /// get 2 boxes with same value
        /// </summary>
        /// <returns></returns>
        private Tuple<Box, Box> FindPair()
        {
            _knownBoxes = _knownBoxes.Distinct().ToList();
            _knownBoxes.RemoveAll(box => box.BoxVisibility == System.Windows.Visibility.Hidden); // remove all taken cards

            var groupBoxesByValueQuery = from box in _knownBoxes
                                         group box by box.Value into boxValues
                                         select new { Count = boxValues.Count(), Boxes = boxValues.ToList() };

            foreach (var boxValue in groupBoxesByValueQuery)
            {
                if (boxValue.Count == 2) // two boxes with same value
                {
                    return new Tuple<Box, Box>(boxValue.Boxes[0], boxValue.Boxes[1]);
                }
            }
            return null;
        }

        /// <summary>
        /// generate random move
        /// </summary>
        /// <returns></returns>
        private Tuple<Box, Box> GenerateTuple()
        {
            _allBoxesIDs = _allBoxesIDs.Distinct().ToList();
            _allBoxesIDs.RemoveAll(boxID => _board.GetBoxByID(boxID).BoxVisibility == System.Windows.Visibility.Hidden); // remove all taken cards

            int index = _generator.Next(0, _allBoxesIDs.Count);
            int boxId1 = _allBoxesIDs.ElementAt(index);
            _allBoxesIDs.Remove(boxId1); // remove -> can't generate two same indexes, then add

            index = _generator.Next(0, _allBoxesIDs.Count);
            int boxId2 = _allBoxesIDs.ElementAt(index);
            _allBoxesIDs.Add(boxId1); // add again

            Box box1 = _board.GetBoxByID(boxId1);
            Box box2 = _board.GetBoxByID(boxId2);

            return new Tuple<Box, Box>(box1, box2);
        }

        /// <summary>
        /// execute move, choose best option
        /// </summary>
        public async Task<bool> ExecuteMove()
        {
            var bestMove = FindPair();
            Box box1, box2;

            if (bestMove == null)
            {
                var randomMove = GenerateTuple();
                box1 = randomMove.Item1;
                box2 = randomMove.Item2;
            }
            else
            {
                box1 = bestMove.Item1;
                box2 = bestMove.Item2;
            }

            box1.Reveal();
            box2.Reveal();
            AddToKnownBoxes(box1);
            AddToKnownBoxes(box2);
            await Task.Delay(500);

            return base.ExecuteMove(box1, box2);
        }

        /// <summary>
        /// add box to knownBoxes
        /// </summary>
        /// <param name="box"></param>
        public void AddToKnownBoxes(Box box)
        {
            if (!_knownBoxes.Contains(box))
            {
                _knownBoxes.Add(box);
            }
        }
    }
}