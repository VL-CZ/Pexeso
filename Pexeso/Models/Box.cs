using System;
using System.Windows;

namespace Pexeso.Models
{
    public class Box : ObservableObject
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

        private string _content;
        /// <summary>
        /// text, that displays on the UI
        /// </summary>
        public string Content
        {
            get { return _content; }
            private set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }

        public Box(int id, int value)
        {
            ID = id;
            Value = value;
            _boxVisibility = Visibility.Visible;
        }

        /// <summary>
        /// remove box from board
        /// </summary>
        public void Disappear()
        {
            BoxVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// reveal content of the box
        /// </summary>
        public void Reveal()
        {
            Content = Value.ToString();
        }

        /// <summary>
        /// hide content of the box
        /// </summary>
        public void Hide()
        {
            Content = String.Empty;
        }
    }
}