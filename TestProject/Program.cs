using TestProject;
using TestProject.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProject.Interfaces;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IDisplayManager, DisplayManager>();
        services.AddTransient<IGridGen, GridGen>();
    })
    .Build();

var gameManager = new GameManager(host.Services.GetRequiredService<IGridGen>(),
                                  host.Services.GetRequiredService<IDisplayManager>());

gameManager.BeginGame();