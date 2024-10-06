using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Project_C_.Games.TicTacToe.Main;

namespace Project_C_.Games.TicTacToe
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            StartGame();
             
        }

        List<ButtonClass> buttonsList = new List<ButtonClass>();

        public Player currentPlayer = Player.Player1;
        public bool GameWon;
        public void PlayerText()
        {
            TurnText.Text = (currentPlayer == Player.Player1) ? "Player X's Turn" : "Player O's Turn";

            SolidColorBrush TurnTextForeground = new SolidColorBrush(
            (currentPlayer == Player.Player1) ?
            (Color)ColorConverter.ConvertFromString("#FFCE4A4A") :
            (Color)ColorConverter.ConvertFromString("#FF4A8FCE"));
            TurnText.Foreground = TurnTextForeground;
        }
        public void SwitchPlayer()
        {
            if (GameWon == false)
            {
                currentPlayer = (currentPlayer == Player.Player1) ? Player.Player2 : Player.Player1;
                PlayerText();
            }
        }
        public void ButtonEventHandler(object sender, RoutedEventArgs e)
        {
            ButtonClass button = sender as ButtonClass;
            SolidColorBrush background = new SolidColorBrush(
            (currentPlayer == Player.Player1) ?
            (Color)ColorConverter.ConvertFromString("#FFCE4A4A") :
            (Color)ColorConverter.ConvertFromString("#FF4A8FCE"));

            if (button != null && button.IsPressed == false)
            {
                button.IsPressed = true;
                button.Content = currentPlayer == Player.Player1 ? "X" : "O";
                button.Background = background;
                Trace.WriteLine(currentPlayer.ToString());
                SwitchPlayer();
                CheckForWinner();
                Trace.WriteLine($"Button Pressed: {button.Name} {button.IsPressed}");
            }
            else
            {
                Trace.WriteLine("the button was pressed already");
            }


        }
        private void StartGame()
        {
            if(GameWon == false)
            {
                BuildButtons();
                currentPlayer = Player.Player1;
                PlayerText();
            }

        }
        private void BuildButtons()
        {
            SolidColorBrush border = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4E669E"));

            int count = 1;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    ButtonClass GridButton = new ButtonClass(col, row, false);
                    GridButton.Content = "";
                    GridButton.Name = "Button" + count.ToString();
                    GridButton.Click += ButtonEventHandler;
                    GridButton.BorderThickness = new Thickness(1);
                    GridButton.BorderBrush = border;
                    GridButton.Background = Brushes.LightGray;

                    Grid.SetColumn(GridButton, col);
                    Grid.SetRow(GridButton, row);
                    ButtonsGrid.Children.Add(GridButton);
                    buttonsList.Add(GridButton);
                    count++;
                }
            }
        }

        private List<ButtonClass> GetButtonsInRow(int row)
        {
            return buttonsList.Where(b => b.Row == row).ToList();
        }
        private List<ButtonClass> GetButtonsInColumn(int col)
        {
            return buttonsList.Where(b => b.Col == col).ToList();
        }
        private List<ButtonClass> GetButtonsInDiagonal1()
        {
            return buttonsList.Where(b => b.Row == b.Col).ToList();
        }
        private List<ButtonClass> GetButtonsInDiagonal2()
        {
            return buttonsList.Where(b => b.Row + b.Col == 2).ToList();
        }
        

        public void CheckForWinner()
        {
            // rows
            for (int row = 0; row < 3; row++)
            {
                
                if (IsWinningLine(GetButtonsInRow(row).Select(b => b.Content.ToString()).ToList()))
                {
                    DeclareWinner(GetButtonsInRow(row).First().Content.ToString());
                    return;
                }
            }

            // columns
            for (int col = 0; col < 3; col++)
            {
                if (IsWinningLine(GetButtonsInColumn(col).Select(b => b.Content.ToString()).ToList()))
                {
                    DeclareWinner(GetButtonsInColumn(col).First().Content.ToString());
                    return;
                }
            }

            // diagonals
            if (IsWinningLine(GetButtonsInDiagonal1().Select(b => b.Content.ToString()).ToList()) ||
                IsWinningLine(GetButtonsInDiagonal2().Select(b => b.Content.ToString()).ToList()))
            {
                DeclareWinner(GetButtonsInDiagonal1().First().Content.ToString());
            }

            if (IsDraw())
            {
                DeclareDraw();
            }
        }
        private bool IsDraw()
        {
            return buttonsList.All(b => b.IsPressed);
        }
        private static bool IsWinningLine(List<string> contents)
        {
            if (contents.All(c => c == "X") || contents.All(c => c == "O"))
            {
                return true;
            }
            return false;
        }
        private void DeclareDraw()
        {
            GameWon = true;
            foreach (var button in buttonsList)
            {
                button.IsEnabled = false; 
            }
            TurnText.Text = "It's a Draw!";
            TurnText.Foreground = Brushes.Orange;
        }
        private void DeclareWinner(string winnerSymbol)
        {
            GameWon = true;
            foreach (var button in buttonsList)
            {
                button.Background = Brushes.Green;
                button.IsEnabled = false;
            }
            if (winnerSymbol == "X")
            {
                TurnText.Text = "Player X Won!";
                TurnText.Foreground = Brushes.Green;

            }
            else if (winnerSymbol == "O")
            {
                TurnText.Text = "Player O Won!";
                TurnText.Foreground = Brushes.Green;
            }
        }
        public void ResetGame()
        {
            foreach (var button in buttonsList)
            {
                button.IsPressed = false;
                button.Content = "";
                button.Background = Brushes.LightGray;
                button.IsEnabled = true;
            }
            TurnText.Text = "";
            currentPlayer = Player.Player1;
            PlayerText();
            GameWon = false;
        }

        public enum Player
        {
            None,
            Player1,
            Player2
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute));
        }
        private void RestartButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResetGame();
        }
        private void ExitButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResetGame();

            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void HideButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
