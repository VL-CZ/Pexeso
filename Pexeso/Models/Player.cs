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
            if (box1.Value == box2.Value)
            {
                box1.MakeTransparent();
                box2.MakeTransparent();
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
