using book_bracket.Models;
using book_bracket.Services;
using book_bracket.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider serviceProvider = DependencyInjection();

IApplication app = serviceProvider.GetRequiredService<IApplication>();

app.Run();

static ServiceProvider DependencyInjection()
{
    ServiceCollection services = new();

    services.AddTransient<IApplication, BookBracketApplication>();
    services.AddTransient<ITournamentFactory, TournamentFactory>();
    services.AddTransient<ITournamentService, TournamentService>();
    services.AddTransient<IUserInterface, ConsoleInterface>();

    return services.BuildServiceProvider();
}