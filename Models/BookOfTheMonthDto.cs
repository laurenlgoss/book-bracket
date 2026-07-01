using book_bracket.Models.Enums;

namespace book_bracket.Models
{
    public class BookOfTheMonthDto : BookDto
    {
        public required Month MonthRead { get; init; }
        public required uint YearRead { get; init; }

        public override string ToString()
        {
            return $"{Name}, {Author} ({MonthRead})";
        }
    }
}