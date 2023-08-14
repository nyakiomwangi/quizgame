using System;
using System.Collections.Generic;
using System.Net;

class Program
{
    
    static void Main()
    {
      


            List<Question> quizQuestions = InitializeQuizQuestions();

        Console.WriteLine("Welcome to the Quiz Application!");
            


            Console.WriteLine("Answer the following questions:\n");

        int score = 0;
        foreach (var question in quizQuestions)
        {
            Console.WriteLine(question.QuestionText);
            for (int i = 0; i < question.Options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Options[i]}");
            }

            Console.Write("Your Answer (Please Key In one of the option numbers ): ");
            int userAnswer;
            while (!int.TryParse(Console.ReadLine(), out userAnswer) || userAnswer < 1 || userAnswer > question.Options.Length)
            {
                Console.Write("Invalid input. Please enter a valid option number: ");
            }

            if (question.IsCorrect(userAnswer))
            {
                Console.WriteLine("Correct!\n");
                score++;
            }
            else
            {
                Console.WriteLine($"Incorrect. The correct answer is: {question.GetCorrectAnswer()}\n");
            }
        }

        Console.WriteLine($"Quiz completed! Your final score: {score} out of {quizQuestions.Count}");
    }

    


    static List<Question> InitializeQuizQuestions()
    {
        List<Question> quizQuestions = new List<Question>();

        // Add your quiz questions here
        quizQuestions.Add(new Question("What is the capital of France?", new string[] { "Berlin", "Paris", "London", "Rome" }, 2));
        quizQuestions.Add(new Question("Which planet is known as the 'Red Planet'?", new string[] { "Mars", "Jupiter", "Venus", "Saturn" }, 1));
        // Add more questions...

        return quizQuestions;
    }
}

class Question
{
    public string QuestionText { get; }
    public string[] Options { get; }
    public int CorrectAnswer { get; }

    public Question(string questionText, string[] options, int correctAnswer)
    {
        QuestionText = questionText;
        Options = options;
        CorrectAnswer = correctAnswer;
    }

    public bool IsCorrect(int userAnswer)
    {
        return userAnswer == CorrectAnswer;
    }

    public string GetCorrectAnswer()
    {
        return Options[CorrectAnswer - 1];
    }
}



