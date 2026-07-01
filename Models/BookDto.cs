namespace book_bracket.Models
{
    public class BookDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Author { get; init; }
        public required string Name { get; init; }
    }
}