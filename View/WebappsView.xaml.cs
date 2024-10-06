using Project_C_.Games.Calculator;
using Project_C_.Games.CountriesAPI;
using Project_C_.Games.MyTasks;
using Project_C_.Games.TicTacToe;
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

namespace Project_C_.View
{
    /// <summary>
    /// Interaction logic for WebappsView.xaml
    /// </summary>
    public partial class WebappsView : UserControl
    {
        public WebappsView()
        {
            InitializeComponent();
        }

        private void App1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = MainWindow.GetWindow(this);
            Frame mainFrame = (Frame)parentWindow.FindName("mainFrame");

            try
            {
                mainFrame.Content = new CountriesPage();
                MainWindow main = new MainWindow();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void App2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = MainWindow.GetWindow(this);
            Frame mainFrame = (Frame)parentWindow.FindName("mainFrame");

            try
            {
                mainFrame.Content = new CalculatorPage();
                MainWindow main = new MainWindow();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void App3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = MainWindow.GetWindow(this);
            Frame mainFrame = (Frame)parentWindow.FindName("mainFrame");

            try
            {
                mainFrame.Content = new MyTasksPage();
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
