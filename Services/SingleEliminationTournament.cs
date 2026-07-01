using book_bracket.Models;
using book_bracket.Services.Interfaces;

namespace book_bracket.Services
{
    public class SingleEliminationTournament : ITournament
    {
        private readonly List<ParticipantDto> _participants;

        public SingleEliminationTournament(IReadOnlyList<ParticipantDto> participants)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(participants.Count, 2);

            _participants = (List<ParticipantDto>)participants;
        }

        public bool IsComplete => Winner is not null;
        public ParticipantDto? Winner { get; private set; } = null;
        public Dictionary<uint, List<MatchDto>> ResultsPerRoundNumber { get; private set; } = [];
        public List<MatchDto> Results => [.. ResultsPerRoundNumber.Values.SelectMany(results => results)];
        public List<Tuple<ParticipantDto, ParticipantDto?>>? CurrentRoundMatches { get; private set; } = [];
        public uint RoundNumber { get; private set; } = 0;

        public void NextRound()
        {
            uint previousRoundNumber = RoundNumber;

            IEnumerable<ParticipantDto> participants;

            if (previousRoundNumber == 0 || ResultsPerRoundNumber.Count == 0)
            {
                participants = _participants;
            }
            else
            {
                participants = ResultsPerRoundNumber[previousRoundNumber].Select(match => match.Winner);
            }

            if (participants.Count() == 1)
            {
                Winner = participants.Single();

                return;
            }

            CurrentRoundMatches = [.. participants.Chunk(2).Select(participants =>
            {
                ParticipantDto? participant2 = participants.Length >= 2 ? participants[1] : null;

                return new Tuple<ParticipantDto, ParticipantDto?>(participants[0], participant2);
            })];

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
    }
}