using book_bracket.Models;
using book_bracket.Services.Interfaces;
using book_bracket.Utilities;

namespace book_bracket.Services
{
    public class BookBracketApplication(IUserInterface userInterface) : IApplication
    {
        private readonly IUserInterface _userInterface = userInterface
            ?? throw new ArgumentNullException(nameof(userInterface));

        public void Run()
        {
            _userInterface.Write("Hello. Let's get started on your book bracket!");

            uint year = GetYear();

            _userInterface.Write($"Thanks. Let's get started on your bracket for {year}.");

            List<FavoriteBook> favoriteBooks = GetFavoriteBooks(year);

            _userInterface.Write("Thanks. Let's get started on comparing them.");

            FavoriteBook winner = StartTournament(favoriteBooks);

            _userInterface.Write($"Congrats! Your {year} winner is {winner.Name} by {winner.Author}.");
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

        private List<FavoriteBook> GetFavoriteBooks(uint year)
        {
            IEnumerable<Month> months = Constants.Months;

            DateOnly now = DateOnly.FromDateTime(DateTime.UtcNow);

            if (now.Year == year)
            {
                months = months.Where(month => (int)month <= now.Month);
            }

            List<FavoriteBook> favoriteBooks = [];

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

                FavoriteBook favoriteBook = new()
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

        private FavoriteBook StartTournament(List<FavoriteBook> favoriteBooks)
        {
            List<Match> matches = [];
            uint roundNumber = 1;

            do
            {
                IEnumerable<FavoriteBook> books =
                    matches.Count != 0 ? matches.Select(match => match.Winner) : favoriteBooks;

                books = books.DistinctBy(book => book.Name);

                matches = StartRound(books, roundNumber);

                roundNumber++;
            }
            while (matches.Count > 1);

            return matches.First().Winner;
        }

        private List<Match> StartRound(IEnumerable<FavoriteBook> favoriteBooks, uint roundNumber)
        {
            const int NumberOfBooksPerMatch = 2;

            _userInterface.Write($"Begin round {roundNumber}.");

            List<Match> matches = [];

            foreach (FavoriteBook[] favoriteBookMatchUp in favoriteBooks.Chunk(NumberOfBooksPerMatch))
            {
                FavoriteBook option1 = favoriteBookMatchUp[0];
                FavoriteBook? option2 = favoriteBookMatchUp.Length >= 2 ? favoriteBookMatchUp[1] : null;

                Match match = StartMatch(option1, option2);

                matches.Add(match);
            }

            return matches;
        }

        private Match StartMatch(FavoriteBook option1, FavoriteBook? option2)
        {
            FavoriteBook matchWinner;

            if (option2 is null)
            {
                _userInterface.Write($"No option 2. Defaulting to {option1.Name} as winner for this match.");

                matchWinner = option1;
            }
            else
            {
                _userInterface.Write($"{option1} vs. {option2}");

                FavoriteBook[] options = [option1, option2];

                string winnerName = _userInterface.Choose("Which book do you prefer?",
                                                          [.. options.Select(option => option.ToString())]);

                matchWinner = options.First(book => winnerName.Contains(book.ToString()));
            }

            _userInterface.Write($"[bold green]Winner: {matchWinner}[/]");

            Match match = new()
            {
                Option1 = option1,
                Option2 = option2,
                Winner = matchWinner,
            };

            return match;
        }
    }
}