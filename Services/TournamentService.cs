using book_bracket.Models;
using book_bracket.Models.Enums;
using book_bracket.Services.Interfaces;

namespace book_bracket.Services
{
    public class TournamentService(ITournamentFactory tournamentFactory,
                                   IUserInterface userInterface) : ITournamentService
    {
        private readonly ITournamentFactory _tournamentFactory = tournamentFactory
            ?? throw new ArgumentNullException(nameof(tournamentFactory));
        private readonly IUserInterface _userInterface = userInterface
            ?? throw new ArgumentNullException(nameof(userInterface));

        public ParticipantDto Start(Tournament type, List<ParticipantDto> participants)
        {
            ITournament tournament = _tournamentFactory.Create(type, participants);

            do
            {
                _userInterface.Write($"--- Begin round {tournament.RoundNumber}. ---");

                foreach (Tuple<ParticipantDto, ParticipantDto?> match in tournament.CurrentRoundMatches ?? [])
                {
                    ParticipantDto winner;

                    if (match.Item2 is null)
                    {
                        _userInterface.Write($"No competition. {match.Item1.Name} advances automatically.");

                        winner = match.Item1;
                    }
                    else
                    {
                        _userInterface.Write($"[bold]{match.Item1.Name}[/] vs. [bold]{match.Item2.Name}[/]");

                        string winnerName = _userInterface.Choose("Which option do you prefer?",
                                                                  match.Item1.Name,
                                                                  match.Item2.Name);

                        winner = winnerName == match.Item1.Name ? match.Item1 : match.Item2;
                    }

                    _userInterface.Write($"[bold green]Winner: {winner.Name}[/]");

                    tournament.RecordResult(match, winner);
                }

                tournament.NextRound();
            } while (!tournament.IsComplete || tournament.Winner is null);

            return tournament.Winner;
        }
    }
}