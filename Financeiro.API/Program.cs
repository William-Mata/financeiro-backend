using Financeiro.API;
using Financeiro.API.Configurations;
using Financeiro.Application;
using Financeiro.Domain.Entities;
using Financeiro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region SERVICES
builder.Services.AddAPI();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<HMACSettings>(builder.Configuration.GetSection("HMACSettings"));
#endregion

#region APP
var app = builder.Build();

app.AddConfigurations();

app.Run();
#endregion