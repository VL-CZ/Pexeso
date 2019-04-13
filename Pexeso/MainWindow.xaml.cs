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

                if (_game.Winner != null)
                {
                    WinnerTextBlock.Visibility = Visibility.Visible;
                }
            }
            else if (_revealed < 2)
            {
                _previousClickedButtonID = buttonID;
                _previousClickedButton = (sender as Button);
            }
        }
    }
}