using book_bracket.Models;
using book_bracket.Services.Interfaces;

namespace book_bracket.Services
{
    public class SingleEliminationTournament : ITournament
    {
        private const uint FirstRoundNumber = 1;

        public SingleEliminationTournament(List<ParticipantDto> participants)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(participants.Count, 2);

            SetCurrentRoundMatches(participants);
        }

        public bool IsComplete => Winner is not null;
        public ParticipantDto? Winner { get; private set; } = null;
        public Dictionary<uint, List<MatchDto>> ResultsPerRoundNumber { get; private set; } = [];
        public List<MatchDto> Results => [.. ResultsPerRoundNumber.Values.SelectMany(results => results)];
        public List<Tuple<ParticipantDto, ParticipantDto?>>? CurrentRoundMatches { get; private set; } = [];
        public uint RoundNumber { get; private set; } = FirstRoundNumber;

        public void NextRound()
        {
            IEnumerable<ParticipantDto> lastRoundWinners =
                ResultsPerRoundNumber[RoundNumber].Select(match => match.Winner);

            if (lastRoundWinners.Count() == 1)
            {
                Winner = lastRoundWinners.Single();

                return;
            }

            SetCurrentRoundMatches(lastRoundWinners);

            RoundNumber++;
        }

        public void RecordResult(Tuple<ParticipantDto, ParticipantDto?> match, ParticipantDto winner)
        {
            MatchDto matchDto = new()
            {
                Participants = [match.Item1, match.Item2],
                Winner = winner,
            };

            if (ResultsPerRoundNumber.TryGetValue(RoundNumber, out List<MatchDto>? matches))
            {
                matches.Add(matchDto);
            }
            else
            {
                ResultsPerRoundNumber.TryAdd(RoundNumber, [matchDto]);
            }
        }

        private void SetCurrentRoundMatches(IEnumerable<ParticipantDto> participants)
        {
            CurrentRoundMatches = [.. participants.Chunk(2).Select(participants =>
            {
                ParticipantDto? participant2 = participants.Length >= 2 ? participants[1] : null;

                return new Tuple<ParticipantDto, ParticipantDto?>(participants[0], participant2);
            })];
        }
    }
}