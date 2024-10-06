using Project_C_.Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;
using Project_C_.Games.Tetris;

namespace Project_C_.Games.Trivia
{
    /// <summary>
    /// Interaction logic for TriviaMain.xaml
    /// </summary>
    public partial class TriviaMain : Window
    {
        private TriviaGame triviaGame;
        private Question currentQuestion;
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;

        private string selectedSubject = "";
        private string selectedDifficulty = "";


        public TriviaMain()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick; 
             
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            timeText.Text = "Time: " + elapsedTime.ToString(@"mm\:ss"); 
        }


        List<Question> worldWondersQuestions = new List<Question>
        {
            new Question("Which is the tallest world wonder?", new List<string>{ "Christ the Redeemer", "Great Wall of China", "Eiffel Tower", "Pyramids of Giza" }, 1),
            new Question("Where is the Colosseum located?", new List<string>{ "Cairo", "Rome", "Paris", "Athens" }, 1),
            new Question("Which wonder is located in India?", new List<string>{ "Taj Mahal", "Petra", "Great Wall of China", "Colosseum" }, 0),
            new Question("What wonder is famous for being carved into rock?", new List<string>{ "Christ the Redeemer", "Great Wall of China", "Petra", "Machu Picchu" }, 2),
            new Question("Which world wonder is found in South America?", new List<string>{ "Pyramids of Giza", "Colosseum", "Petra", "Machu Picchu" }, 3),
            new Question("Which wonder took 20 years to build?", new List<string>{ "Christ the Redeemer", "Great Wall of China", "Taj Mahal", "Colosseum" }, 2),
            new Question("What wonder is considered the longest man-made structure in the world?", new List<string>{ "Petra", "Colosseum", "Pyramids of Giza", "Great Wall of China" }, 3),
            new Question("Which wonder is located in Rio de Janeiro?", new List<string>{ "Great Wall of China", "Christ the Redeemer", "Colosseum", "Petra" }, 1),
            new Question("Which wonder is the only one still largely intact?", new List<string>{ "Petra", "Colosseum", "Great Wall of China", "Pyramids of Giza" }, 3),
            new Question("Which world wonder is also known as 'The Lost City'?", new List<string>{ "Great Wall of China", "Petra", "Machu Picchu", "Colosseum" }, 2)
        };

        List<Question> ancientCivilizationQuestions = new List<Question>
        {
            new Question("Which civilization built the Pyramids of Giza?", new List<string>{ "Greeks", "Babylonians", "Romans", "Egyptians" }, 3),
            new Question("Which city was home to the famous Hanging Gardens?", new List<string>{ "Babylon", "Alexandria", "Athens", "Rome" }, 0),
            new Question("Which civilization is credited with creating the first written language?", new List<string>{ "Sumerians", "Greeks", "Romans", "Egyptians" }, 0),
            new Question("Which civilization built the Parthenon?", new List<string>{ "Greeks", "Persians", "Egyptians", "Romans" }, 0),
            new Question("Who was the famous ruler of the Babylonian Empire known for his code of laws?", new List<string>{ "Julius Caesar", "Hammurabi", "Alexander the Great", "Ramses II" }, 1),
            new Question("The city of Troy is most famous for which historical event?", new List<string>{ "The Persian Wars", "The Fall of Babylon", "Trojan War", "The Battle of Thermopylae" }, 2),
            new Question("Which civilization built the Ziggurats, large terraced structures?", new List<string>{ "Egyptians", "Phoenicians", "Romans", "Sumerians" }, 3),
            new Question("Which civilization built the Acropolis?", new List<string>{ "Romans", "Egyptians", "Greeks", "Persians" }, 2),
            new Question("The Great Library of Alexandria was built in which ancient civilization?", new List<string>{ "Rome", "Persia", "Egypt", "Greece" }, 2),
            new Question("The first Olympic Games were held in which ancient city?", new List<string>{ "Olympia", "Athens", "Sparta", "Rome" }, 0)
        };


        private void StartButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List<Question> questionList = new List<Question>();

            if (selectedSubject == "World Wonders")
                questionList = worldWondersQuestions;
            else if (selectedSubject == "Ancient Civilizations")
                questionList = ancientCivilizationQuestions;
            else
            {
                MessageBox.Show("Please select a subject to start the game.");
                return;
            }

            List<Question> finalQuestions = new List<Question>();
            switch (selectedDifficulty)
            {
                case "Easy":
                    finalQuestions = questionList.Take(3).ToList();  
                    break;
                case "Medium":
                    finalQuestions = questionList.Take(5).ToList();  
                    break;
                case "Hard":
                    finalQuestions = questionList;
                    break;
                default:
                    MessageBox.Show("Please select a difficulty to start the game.");
                    return;
            }

            triviaGame = new TriviaGame(finalQuestions);
            elapsedTime = TimeSpan.Zero; 
            timer.Start();
            ShowNextQuestion();
            StartButton.Visibility = Visibility.Hidden;
        }

        private void ShowNextQuestion()
        {

            if (triviaGame.HasMoreQuestions())
            {
                currentQuestion = triviaGame.GetNextQuestion();
                questionText.Text = currentQuestion.Text;
                scoreText.Text = "Score: " + triviaGame.Score.ToString();
                subjectText.Text = "Subject: " + selectedSubject;
                difficultyText.Text = "Difficulty: " + selectedDifficulty;
                Answer1.Content = currentQuestion.Options[0];
                Answer2.Content = currentQuestion.Options[1];
                Answer3.Content = currentQuestion.Options[2];
                Answer4.Content = currentQuestion.Options[3];
            }
            else
            {
                timer.Stop();
                scoreText.Text = "Score: " + triviaGame.Score.ToString();
                GameOverMenu.Visibility = Visibility.Visible;
            }
        }

        private void CheckAnswer(int answerIndex)
        {
            if (triviaGame == null)
            {
                return;
            }
            else
            {
                triviaGame.CheckAnswer(answerIndex);
                ShowNextQuestion();
            }
        }

        private void AnswerButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer(0);
        }

        private void AnswerButton2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer(1);
        }

        private void AnswerButton3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer(2);
        }

        private void AnswerButton4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer(3);
        }

        private void RestartButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            triviaGame = null;
            StartButton_MouseLeftButtonDown(sender, e);
            GameOverMenu.Visibility = Visibility.Hidden;
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            selectedSubject = selectedItem.Content.ToString();
        }

        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            selectedDifficulty = selectedItem.Content.ToString(); ;
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
