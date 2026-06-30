namespace book_bracket.Models
{
    public class Match
    {
        public required FavoriteBook Option1 { get; init; }
        public required FavoriteBook? Option2 { get; init; }
        public required FavoriteBook Winner { get; set; }
    }
}