namespace book_bracket.Models
{
    public class MatchDto
    {
        public required List<ParticipantDto?> Participants { get; init; }
        public required ParticipantDto Winner { get; set; }
    }
}