using book_bracket.Models;

namespace book_bracket.Services.Interfaces
{
    public interface ITournament
    {
        bool IsComplete { get; }
        ParticipantDto? Winner { get; }
        Dictionary<uint, List<MatchDto>> ResultsPerRoundNumber { get; }
        List<MatchDto> Results { get; }
        List<Tuple<ParticipantDto, ParticipantDto?>>? CurrentRoundMatches { get; }
        uint RoundNumber { get; }

        void NextRound();
        void RecordResult(Tuple<ParticipantDto, ParticipantDto?> match, ParticipantDto winner);
    }
}