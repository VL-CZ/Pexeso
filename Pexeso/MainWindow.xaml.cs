using Pexeso.Models;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            _game.Board.RevealBox(buttonID);

            if (_revealed == 2)
            {
                await Task.Delay(500); // wait for 0.5s
                await _game.ExecuteMove(_previousClickedButtonID, buttonID);
                _revealed = 0;

                if (_game.Winner != null)
                {
                    WinnerTextBlock.Visibility = Visibility.Visible;
                }
            }
            else if (_revealed < 2)
            {
                _previousClickedButtonID = buttonID;
            }
        }
    }
}