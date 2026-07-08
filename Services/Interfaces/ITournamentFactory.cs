using book_bracket.Models;
using book_bracket.Models.Enums;

namespace book_bracket.Services.Interfaces
{
    public interface ITournamentFactory
    {
        ITournament Create(TournamentType type, List<ParticipantDto> participants);
    }
}