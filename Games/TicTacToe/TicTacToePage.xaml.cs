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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_C_.Games.TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToePage.xaml
    /// </summary>
    public partial class TicTacToePage : Page
    {
        public TicTacToePage()
        {
            InitializeComponent();
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Window parentWindow = MainWindow.GetWindow(this);
                parentWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HideButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Window parentWindow = MainWindow.GetWindow(this);
                parentWindow.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main win = new Main();
            win.Show();
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();

            try
            {
                Window parentWindow = MainWindow.GetWindow(this);
                parentWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
