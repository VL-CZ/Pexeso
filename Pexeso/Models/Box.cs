using Pexeso.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pexeso.Models
{
    class Box : ObservableObject
    {
        private Visibility _boxVisibility;
        /// <summary>
        /// Is this box visible?
        /// note: this property is used only for binding
        /// </summary>
        public Visibility BoxVisibility
        {
            get => _boxVisibility;
            set
            {
                _boxVisibility = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// value of the box (two cells make pair if their values are equal)
        /// </summary>
        public int Value { get; }

        public int ID { get; }

        public Box(int id, int value)
        {
            ID = id;
            Value = value;
            _boxVisibility = Visibility.Visible;
        }

        public void MakeTransparent()
        {
            BoxVisibility = Visibility.Hidden;
        }
    }
}
