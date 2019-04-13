using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class Player : ObservableObject
    {
        private int _score;
        /// <summary>
        /// score of the player
        /// </summary>
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged();
            }
        }

        public Player()
        {
            Score = 0;
        }

        /// <summary>
        /// execute move of the player
        /// </summary>
        /// <param name="box1"></param>
        /// <param name="box2"></param>
        /// <returns>do boxes values match?</returns>
        public virtual bool ExecuteMove(Box box1, Box box2)
        {
            if ((box1.Value == box2.Value) && (box1 != box2))
            {
                box1.Disappear();
                box2.Disappear();
                Score++;
                return true;
            }
            else
            {
                box1.Hide();
                box2.Hide();
                return false;
            }
        }
    }
}
