using book_bracket.Services.Interfaces;
using Spectre.Console;

namespace book_bracket.Services
{
    /// <summary>
    /// Interacts with user via <see cref="Console"/>.
    /// </summary>
    public class ConsoleInterface : IUserInterface
    {
        public string Choose(string title, params string[] choices)
        {
            if (choices.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(choices));
            }

            SelectionPrompt<string> prompt = new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices);

            return AnsiConsole.Prompt(prompt);
        }

        public string? Read(string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                AnsiConsole.MarkupLine(message);
            }

            return Console.ReadLine();
        }

        public void Warn(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);

            Console.Error.WriteLine(message);
        }

        public void Write(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);

            AnsiConsole.MarkupLine(message);
        }
    }
}