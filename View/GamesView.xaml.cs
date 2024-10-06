using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
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
using System.Windows.Navigation;
using Project_C_.Games.TicTacToe;
using Project_C_.Games.Trivia;
using Project_C_.Games.MyTasks;
using Project_C_.Games.CountriesAPI;
using Project_C_.Games.Tetris;
using Project_C_.Games.MemoryCards;



namespace Project_C_.View
{
    /// <summary>
    /// Interaction logic for GamesView.xaml
    /// </summary>
    public partial class GamesView : UserControl
    {
        public Uri Source { get; set; }

        public GamesView()
        {
            InitializeComponent();
        }

        private void Game2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = MainWindow.GetWindow(this);
            Frame mainFrame = (Frame)parentWindow.FindName("mainFrame");

            try
            {
                mainFrame.Content = new TriviaPage();
                MainWindow main = new MainWindow();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Game3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Window parentWindow = MainWindow.GetWindow(this);
            Frame mainFrame = (Frame)parentWindow.FindName("mainFrame");

            try
            {
                mainFrame.Content = new TicTacToePage();
                MainWindow main = new MainWindow();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Game1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = MainWindow.GetWindow(this);
            Frame mainFrame = (Frame)parentWindow.FindName("mainFrame");

            try
            {
                mainFrame.Content = new MemoryGamePage();
                MainWindow main = new MainWindow();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Game4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = MainWindow.GetWindow(this);
            Frame mainFrame = (Frame)parentWindow.FindName("mainFrame");

            try
            {
                mainFrame.Content = new TetrisPage();
                MainWindow main = new MainWindow();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }

    
}
