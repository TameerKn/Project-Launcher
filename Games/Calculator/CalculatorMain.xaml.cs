using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Project_C_.Games.Calculator
{
    /// <summary>
    /// Interaction logic for CalculatorMain.xaml
    /// </summary>
    public partial class CalculatorMain : Window
    {

        double resultValue = 0;
        string selectedOperator = "";
        bool isOperationPerformed = false;

        public CalculatorMain()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (valueBox.Text == "0" || isOperationPerformed )
                valueBox.Text = "";

            isOperationPerformed = false;
            Button button = (Button)sender;
            valueBox.Text = valueBox.Text + button.Content.ToString();

        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            selectedOperator = button.Content.ToString();
            resultValue = Double.Parse(valueBox.Text);
            resultText.Content = resultValue + " " + selectedOperator;
            isOperationPerformed = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            switch (selectedOperator)
            {
                case "+":
                    resultText.Content = (resultValue + Double.Parse(valueBox.Text)).ToString();
                    break;
                case "-":
                    resultText.Content = (resultValue - Double.Parse(valueBox.Text)).ToString();
                    break;
                case "*":
                    resultText.Content = (resultValue * Double.Parse(valueBox.Text)).ToString();
                    break;
                case "/":
                    resultText.Content = (resultValue / Double.Parse(valueBox.Text)).ToString();
                    break;
                default:
                    break;
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            valueBox.Text = "0";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            valueBox.Text = "0";
            resultText.Content = "0";
            resultValue = 0;
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
