using book_bracket.Models;
using book_bracket.Models.Enums;
using book_bracket.Services.Interfaces;
using book_bracket.Utilities;

namespace book_bracket.Services
{
    public class BookBracketApplication(IUserInterface userInterface,
                                        ITournamentService tournamentService) : IApplication
    {
        private readonly IUserInterface _userInterface = userInterface
            ?? throw new ArgumentNullException(nameof(userInterface));
        private readonly ITournamentService _tournamentService = tournamentService
            ?? throw new ArgumentNullException(nameof(tournamentService));

        public void Run()
        {
            _userInterface.Write("Hello. Let's get started on your book bracket!");

            uint year = GetYear();

            _userInterface.Write($"Thanks. Let's get started on your bracket for {year}.");

            List<BookOfTheMonthDto> favoriteBooks = GetFavoriteBooks(year);

            _userInterface.Write("Thanks. Let's get started on comparing them.");

            string tournamentChoice = _userInterface.Choose("What kind of tournament do you want to use?",
                                                            [.. Enum.GetValues<Tournament>().Select(type => type.ToString())]);

            _ = Enum.TryParse(tournamentChoice, true, out Tournament tournamentType);

            List<ParticipantDto> participants = [.. favoriteBooks.Select(book =>
            {
                return new ParticipantDto()
                {
                  Id = book.Id,
                  Name = book.ToString(),  
                };
            })];

            ParticipantDto tournamentWinner = _tournamentService.Start(tournamentType, participants);

            BookOfTheMonthDto bookOfTheYear = favoriteBooks.First(book => book.Id == tournamentWinner.Id);

            _userInterface.Write($"[bold green]Congrats! Your {year} winner is {bookOfTheYear.Name} by {bookOfTheYear.Author}.[/]");
        }

        private uint GetYear()
        {
            string? message =
                _userInterface.Read("What year do you want to create a bracket for?");

            uint year;

            while (!uint.TryParse(message, out year)
                || year > DateTime.MaxValue.Year
                || year < DateTime.MinValue.Year)
            {
                _userInterface.Warn("Please input a valid year.");

                message = _userInterface.Read();
            }

            return year;
        }

        private List<BookOfTheMonthDto> GetFavoriteBooks(uint year)
        {
            IEnumerable<Month> months = Constants.Months;

            DateOnly now = DateOnly.FromDateTime(DateTime.UtcNow);

            if (now.Year == year)
            {
                months = months.Where(month => (int)month <= now.Month);
            }

            List<BookOfTheMonthDto> favoriteBooks = [];

            foreach (Month month in months)
            {
                _userInterface.Write($"Favorite book from {month}, {year}:");

                string? name = _userInterface.Read("Name:");

                while (string.IsNullOrWhiteSpace(name))
                {
                    _userInterface.Warn("Please input a valid name.");

                    name = _userInterface.Read();
                }

                string? author = _userInterface.Read("Author:");

                while (string.IsNullOrWhiteSpace(author))
                {
                    _userInterface.Warn("Please input a valid author.");

                    author = _userInterface.Read();
                }

                BookOfTheMonthDto favoriteBook = new()
                {
                    Author = author,
                    Name = name,
                    MonthRead = month,
                    YearRead = year,
                };

                favoriteBooks.Add(favoriteBook);
            }

            return favoriteBooks;
        }
    }
}