namespace book_bracket.Models
{
    public class FavoriteBook : Book
    {
        public required Month MonthRead { get; init; }
        public required uint YearRead { get; init; }

        public override string ToString()
        {
            return $"{Name}, {Author} ({MonthRead})";
        }
    }
}