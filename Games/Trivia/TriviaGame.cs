using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Project_C_.Games.Trivia
{
    public class TriviaGame : TriviaMain
    {

        private List<Question> questions;
        private int currentQuestionIndex;
        public int Score { get; private set; }

        public TriviaGame(List<Question> questionList)
        {
            questions = questionList;
            currentQuestionIndex = 0;
            Score = 0;
        }

        public Question GetNextQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                return questions[currentQuestionIndex++];
            }
            return null; 
        }

        public void CheckAnswer(int answerIndex)
        {
            Question currentQuestion = questions[currentQuestionIndex - 1];
            if (currentQuestion.IsCorrect(answerIndex))
            {
                Score++;
            }
        }

        public bool HasMoreQuestions()
        {
            return currentQuestionIndex < questions.Count;
        }

    }
}
