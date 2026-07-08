using book_bracket.Models;
using book_bracket.Models.Enums;

namespace book_bracket.Services.Interfaces
{
    public interface ITournamentService
    {
        /// <summary>
        /// Starts tournament with <paramref name="participants"/>.
        /// </summary>
        /// <param name="type">Type of tournament.</param>
        /// <param name="participants">Tournament participants.</param>
        /// <returns>Tournament winner.</returns>
        ParticipantDto Start(TournamentType type, List<ParticipantDto> participants);
    }
}