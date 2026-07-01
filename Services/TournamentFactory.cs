using book_bracket.Models;
using book_bracket.Models.Enums;
using book_bracket.Services.Interfaces;

namespace book_bracket.Services
{
    public class TournamentFactory(IUserInterface userInterface) : ITournamentFactory
    {
        private readonly IUserInterface _userInterface = userInterface
            ?? throw new ArgumentNullException(nameof(userInterface));

        public ITournament Create(Tournament type, List<ParticipantDto> participants)
        {
            return type switch
            {
                Tournament.SingleElimination => new SingleEliminationTournament(participants),
                _ => throw new NotImplementedException(),
            };
        }
    }
}