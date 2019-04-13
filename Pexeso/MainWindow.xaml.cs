using Pexeso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pexeso
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;
        private int _revealed = 0;
        private int _previousClickedButtonID;
        private Button _previousClickedButton;

        public MainWindow()
        {
            InitializeComponent();
            _game = new Game();
            DataContext = _game;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int buttonID = int.Parse(((Button)sender).Tag.ToString());
            _revealed++;
            (sender as Button).Content = _game.Board.GetBoxByID(buttonID).Value;

            if (_revealed == 2)
            {
                await Task.Delay(500); // wait for 0.5s
                _game.ExecuteMove(_previousClickedButtonID, buttonID);
                if (!_game.IsLastMovePair)
                {
                    _previousClickedButton.Content = "";
                    (sender as Button).Content = "";
                }
                _revealed = 0;
            }
            else if (_revealed < 2)
            {
                _previousClickedButtonID = buttonID;
                _previousClickedButton = (sender as Button);
            }
        }
    }
}
