using book_bracket.Models;

namespace book_bracket.Services.Interfaces
{
    /// <summary>
    /// Manages tournament state.
    /// </summary>
    public interface ITournament
    {
        /// <summary>
        /// Returns <see langword="true"/> if tournament is complete.
        /// </summary>
        bool IsComplete { get; }
        /// <summary>
        /// Tournament winner. Returns <see langword="null"/> if tournament is not complete.
        /// </summary>
        ParticipantDto? Winner { get; }
        /// <summary>
        /// Match results per round number.
        /// </summary>
        Dictionary<uint, List<MatchDto>> ResultsPerRoundNumber { get; }
        /// <summary>
        /// All round match results.
        /// </summary>
        List<MatchDto> Results { get; }
        /// <summary>
        /// Current round matches.
        /// </summary>
        List<Tuple<ParticipantDto, ParticipantDto?>>? CurrentRoundMatches { get; }
        /// <summary>
        /// Current round number.
        /// </summary>
        uint RoundNumber { get; }

        /// <summary>
        /// Proceeds to next round.
        /// </summary>
        void NextRound();
        /// <summary>
        /// Records match result.
        /// </summary>
        /// <param name="match">Current match.</param>
        /// <param name="winner">Winner of current match.</param>
        void RecordMatchResult(Tuple<ParticipantDto, ParticipantDto?> match, ParticipantDto winner);
    }
}