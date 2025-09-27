using Financeiro.API;
using Financeiro.Application;
using Financeiro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region SERVICES
builder.Services.AddAPI();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region APP
var app = builder.Build();

app.AddConfigurations();

app.Run();
#endregion