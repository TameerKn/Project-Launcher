using Project_C_.Games.Tetris;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace Project_C_.Games.MemoryCards
{
    /// <summary>
    /// Interaction logic for MemoryCardsMain.xaml
    /// </summary>
    public partial class MemoryCardsMain : Window
    {


        private Button firstClicked, secondClicked;
        private SolidColorBrush[] cardColors;
        private Random random = new Random();
        private int moveCount = 0;


        public MemoryCardsMain()
        {
            InitializeComponent();
            InitializeCardGrid();
            AssignColorsToCards();
        }

        private void InitializeCardGrid()
        {
            for (int i = 0; i < 4; i++)
            {
                CardGrid.RowDefinitions.Add(new RowDefinition());
                CardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Button cardButton = new Button
                    {
                        Background = Brushes.Gray,
                        Margin = new Thickness(1),
                    };

                    cardButton.Click += Card_Click;

                    Grid.SetRow(cardButton, row);
                    Grid.SetColumn(cardButton, col);
                    CardGrid.Children.Add(cardButton);
                }
            }
        }

        private void AssignColorsToCards()
        {
            cardColors = new SolidColorBrush[]
            {
                Brushes.Red, Brushes.Red,
                Brushes.Green, Brushes.Green,
                Brushes.Blue, Brushes.Blue,
                Brushes.Yellow, Brushes.Yellow,
                Brushes.Purple, Brushes.Purple,
                Brushes.Orange, Brushes.Orange,
                Brushes.Cyan, Brushes.Cyan,
                Brushes.Pink, Brushes.Pink
            };

            cardColors = ShuffleColors(cardColors);

            for (int i = 0; i < CardGrid.Children.Count; i++)
            {
                Button cardButton = (Button)CardGrid.Children[i];
                cardButton.Tag = cardColors[i];
            }
        }

        private void CheckGameOver()
        {
            foreach (Button cardButton in CardGrid.Children)
            {
                if (cardButton.Background == Brushes.Gray)
                {
                    return;
                }
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalMovesText.Text = $"Moves: {moveCount}";

        }

        private SolidColorBrush[] ShuffleColors(SolidColorBrush[] colors)
        {
            for (int i = colors.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                SolidColorBrush temp = colors[i];
                colors[i] = colors[j];
                colors[j] = temp;
            }
            return colors;
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            Button clickedCard = sender as Button;

                if (clickedCard.Background != Brushes.Gray || secondClicked != null)
                return;

            clickedCard.Background = (SolidColorBrush)clickedCard.Tag;

            if (firstClicked == null)
            {
                firstClicked = clickedCard;
            }
            else
            {
                secondClicked = clickedCard;

                moveCount++;
                MovesText.Content = $"Moves: {moveCount}";


                if (firstClicked.Background == secondClicked.Background)
                {
                    firstClicked = null;
                    secondClicked = null;

                    CheckGameOver();
                }
                else
                {
                    Dispatcher.InvokeAsync(async () =>
                    {
                        await Task.Delay(1000);
                        firstClicked.Background = Brushes.Gray;
                        secondClicked.Background = Brushes.Gray;
                        firstClicked = null;
                        secondClicked = null;
                    });
                }
            }
        }

        private void RestartGame()
        {
            firstClicked = null;
            secondClicked = null;

            AssignColorsToCards();

            foreach (Button cardButton in CardGrid.Children)
            {
                cardButton.Background = Brushes.Gray;
            }
            GameOverMenu.Visibility = Visibility.Hidden;
            moveCount = 0;
            MovesText.Content = $"Moves: {moveCount}";

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute));

        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute));

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void RestartButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestartGame();
        }

        private void PlayAgainButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestartGame();
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

        private void ExitButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
