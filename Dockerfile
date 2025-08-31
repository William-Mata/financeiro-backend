# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solução e projetos
COPY *.sln .
COPY Financeiro.API/*.csproj ./Financeiro.API/
COPY Financeiro.Testes/*.csproj ./Financeiro.Testes/

# Restaurar dependências
RUN dotnet restore

# Copiar código fonte
COPY . .

# Build e publicar
WORKDIR /Financeiro.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Criar usuário não-root
RUN addgroup --system --gid 1001 appgroup && \
    adduser --system --uid 1001 appuser --gid 1001
USER appuser

# Copiar aplicação
COPY --from=build /app/publish .

# Configurações
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production


# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=30s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "Financeiro.API.dll"]