using Financeiro.API;
using Financeiro.Application;
using Financeiro.Domain.Entities.Configuracoes;
using Financeiro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region SERVICES
builder.Services.AddAPI()
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"))
                .Configure<HMACSettings>(builder.Configuration.GetSection("HMACSettings"));
#endregion

#region APP
var app = builder.Build();

app.AddConfigurations();

app.Run();
#endregion