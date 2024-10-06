using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
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

namespace Project_C_.Games.Tetris
{
    /// <summary>
    /// Interaction logic for TetrisWindow.xaml
    /// </summary>
    public partial class TetrisWindow : Window
    {

        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))

        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };

        private readonly Image[,] ImageControls;

        private GameState gameState = new GameState();

        public TetrisWindow()
        {
            InitializeComponent();
            ImageControls = SetUpGameCanvas(gameState.GameGrid);
        }

        private Image[,] SetUpGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            
            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for( int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0 ; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    ImageControls[r, c].Opacity = 1;
                    ImageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Blocks block)
        {
            foreach(Position p in block.TilePositions())
            {
                ImageControls[p.Row, p.Column].Opacity = 1;
                ImageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Blocks nextBlock = blockQueue.NextBlock;
            NextImage.Source = blockImages[nextBlock.Id];
        }

        private void DrawHeldBlock(Blocks heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.Id];
            }
        }

        private void DrawDropIndicator(Blocks block)
        {
            int dropDistance = gameState.BlockDropDistance();

            foreach (Position p in block.TilePositions())
            {
                ImageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                ImageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawDropIndicator(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            scoreText.Content = "Score: " + gameState.Score; 
        }

        private bool isPaused = false;
        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                await Task.Delay(500);

                if (isPaused)
                {
                    await Task.Delay(100);
                    continue;
                }

                gameState.MoveBlockDown();
                Draw(gameState);
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = "Score: " + gameState.Score;
        }

        private async void ContinueGame()
        {
            GamePaused.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Trace.WriteLine($"Key pressed: {e.Key}");
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    Trace.WriteLine("block down presset");
                    break;
                case Key.Up:
                    gameState.RotateBlockCW();
                    break;
                case Key.R:
                    gameState.RotateBlockCCW();
                    break;
                case Key.E:
                    gameState.holdBlock();
                    break;
                case Key.Space:
                    gameState.DropBlock();
                    break;
                default:
                    return;
            }

            Draw(gameState);
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility= Visibility.Hidden;
            await GameLoop();
        }

        private async void StartButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartButton.Visibility= Visibility.Hidden;
            await GameLoop();
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

        private void PauseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isPaused)
            {
                isPaused = true;
                pauseText.Content = "Resume";
                GamePaused.Visibility = Visibility.Visible;
            }
            else
            {
                isPaused = false;
                pauseText.Content = "Pause";
                ContinueGame();
                GamePaused.Visibility = Visibility.Hidden;
            }
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
