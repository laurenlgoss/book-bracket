using book_bracket.Models;
using book_bracket.Models.Enums;

namespace book_bracket.Services.Interfaces
{
    public interface ITournamentService
    {
        ParticipantDto Start(Tournament type, List<ParticipantDto> participants);
    }
}