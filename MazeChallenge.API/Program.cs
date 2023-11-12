using MazeChallenge.API.Installers;

var builder = WebApplication.CreateBuilder(args);

var startup = new MazeChallenge.API.StartUp(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
await PopupInMemoryDb.Populate(app);
startup.Configure(app, builder.Environment);
