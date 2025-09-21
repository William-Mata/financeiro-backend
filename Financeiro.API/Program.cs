using Financeiro.Application;
using Financeiro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region SERVICES
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

#endregion

#region APP
var app = builder.Build();

// CONFIGURAÇÃO DO SWAGGER PARA DESENVOLVIMENTO E PRODUÇÃO
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
#endregion