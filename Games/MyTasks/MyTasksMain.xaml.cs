using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
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

namespace Project_C_.Games.MyTasks
{
    /// <summary>
    /// Interaction logic for MyTasksMain.xaml
    /// </summary>
    public partial class MyTasksMain : Window
    {
        private TaskManagerService _todoList;

        public MyTasksMain()
        {
            InitializeComponent();
            InitializeTasks();
             
        }

        private void InitializeTasks()
        {
            _todoList = new TaskManagerService();
            listTasks.ItemsSource = _todoList.Tasks;
        }

        //OnTaskToggled
        //get the task (and task id) from the checkBox.DataContext (the DataContext is actually the TodoTask object)
        //excute the toggle function from the model
        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskModel task)
            {
                _todoList.ToggleTaskIsComplete(task.Id);
            }
        }



        private void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //get the elements
                TextBlock textBlock = sender as TextBlock;
                StackPanel parent = textBlock.Parent as StackPanel;
                TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
                Button btnSave = parent.FindName("btnSave") as Button;
                Button btnDelete = parent.FindName("btnDelete") as Button;
                //hide the textBlock
                textBlock.Visibility = Visibility.Collapsed;
                //show the text Box & save button
                editTextBox.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                //show int the TextBox.Text the task description
                editTextBox.Text = textBlock.Text;
            }
        }



        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            //get the elements
            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            Button btnDelete = parent.FindName("btnDelete") as Button;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            TaskModel task = textBlock.DataContext as TaskModel;

            //Hide the TextBox
            editTextBox.Visibility = Visibility.Collapsed;
            //Hide the save button
            btnSave.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            //Show the TextBlock
            textBlock.Visibility = Visibility.Visible;

            //take the TextBox.Text and put in the TextBlock.Text
            textBlock.Text = editTextBox.Text;
            _todoList.UpdateTask(task.Id, editTextBox.Text);
        }



        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewTask.Text))
            {
                //Create the new Task 
                TaskModel newTask = new TaskModel(_todoList.Tasks.Count + 1, txtNewTask.Text);
                _todoList.AddNewTask(newTask);
                txtNewTask.Clear();
            }

        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
           Button deleteButton = sender as Button;
           StackPanel parent = deleteButton.Parent as StackPanel;
           TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
           TaskModel task = textBlock.DataContext as TaskModel;

            _todoList.RemoveTask(task);

            listTasks.ItemsSource = null;
            listTasks.ItemsSource = _todoList.Tasks;
        }

        private void chkTask_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            ThemeSwitch.SwitchTheme(new Uri("Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute));
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
