using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Linq;


class Program
{
    static void Main()
    {
        using (var context = new QuizDbContext())
        {
            InitializeQuizQuestions(context);

            Console.WriteLine("Welcome to the Quiz Application!");
            Console.WriteLine("Here are C# questions: Answer the following questions:\n");

            int totalScore = 0;
            var quizQuestions = context.Questions.ToList();

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
                    totalScore++;
                }
                else
                {
                    Console.WriteLine($"Incorrect. The correct answer is: {question.GetCorrectAnswer()}\n");
                }
            }

            Console.WriteLine($"Quiz completed! Your final score: {totalScore} out of {quizQuestions.Count}");
        }
    }

    static void InitializeQuizQuestions(QuizDbContext context)
    {
        context.Database.EnsureCreated(); // makes sure the database exists



        if (!context.Questions.Any())
        {
            context.Questions.Add(new QuestionEntity
            {
                QuestionText = " What is the extension of a C# language file?",
                Options = new string[] { ".c", ".cpp", ".cs", ".csp" },
                CorrectAnswer = 3
            });

            context.Questions.Add(new QuestionEntity
            {
                QuestionText = "C# is a programming language programmed by?",
                Options = new string[] { "Microsoft", "Oracle", "Google", "IBM" },
                CorrectAnswer = 1
            });


            context.Questions.Add(new QuestionEntity
            {
                QuestionText = "SOAP in C# stands for ___",
                Options = new string[] { "Simple Object Access Protocol", "Simple Object Access Program", "Simple Object Access Protocal", "Standard Object Access Protocol" },
                CorrectAnswer = 1
            });

            context.Questions.Add(new QuestionEntity
            {
                QuestionText = "Which symbols are used to mark the beginning and end of a code block?",
                Options = new string[] { "Berlin", "Paris", "London", "Rome" },
                CorrectAnswer = 2
            });

            context.Questions.Add(new QuestionEntity
            {
                QuestionText = "What is the capital of France?",
                Options = new string[] { "Berlin", "Paris", "London", "Rome" },
                CorrectAnswer = 2
            });

            context.Questions.Add(new QuestionEntity
            {
                QuestionText = "What is the capital of France?",
                Options = new string[] { "Berlin", "Paris", "London", "Rome" },
                CorrectAnswer = 2
            });

            

            context.Questions.Add(new QuestionEntity
            {
                QuestionText = "C# runs on the",
                Options = new string[] { "Java Virtual Machine", ".NET Framework", "Both 1 and 2", "None" },
                CorrectAnswer = 2
            });

            context.Questions.Add(new QuestionEntity
            {
                QuestionText = "What is the capital of France?",
                Options = new string[] { "Berlin", "Paris", "London", "Rome" },
                CorrectAnswer = 2
            });

            context.SaveChanges();
        }
    }
}

class QuizDbContext : DbContext
{
    public DbSet<QuestionEntity> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=quiz.db");
    }
}

class QuestionEntity
{
    public int Id { get; set; }
    public required string QuestionText { get; set; }
    public required string[] Options { get; set; }
    public int CorrectAnswer { get; set; }

    public bool IsCorrect(int userAnswer)
    {
        return userAnswer == CorrectAnswer;
    }

    public string GetCorrectAnswer()
    {
        return Options[CorrectAnswer - 1];
    }
}
