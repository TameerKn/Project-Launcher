using System.Text;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_C_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Uri Source { get; set; }

        public MainWindow()
        {
            InitializeComponent(); 
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

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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