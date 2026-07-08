using book_bracket.Models;
using book_bracket.Models.Enums;
using book_bracket.Services.Interfaces;

namespace book_bracket.Services
{
    public class TournamentFactory : ITournamentFactory
    {
        public ITournament Create(TournamentType type, List<ParticipantDto> participants)
        {
            return type switch
            {
                TournamentType.SingleElimination => new SingleEliminationTournament(participants),
                _ => throw new NotImplementedException(),
            };
        }
    }
}