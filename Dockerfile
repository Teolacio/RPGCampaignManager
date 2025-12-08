# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos de projeto (csproj) de cada camada
COPY ["RPGCM.API/RPGCM.API.csproj", "RPGCM.API/"]
COPY ["RPGCM.Aplication/RPGCM.Aplication.csproj", "RPGCM.Aplication/"]
COPY ["RPGCM.Domain/RPGCM.Domain.csproj", "RPGCM.Domain/"]
COPY ["RPGCM.Infrastructure/RPGCM.Infrastructure.csproj", "RPGCM.Infrastructure/"]

# Restaurar dependências
RUN dotnet restore "RPGCM.API/RPGCM.API.csproj"

# Copiar o restante do código
COPY . .

# Compilar
WORKDIR "/src/RPGCM.API"
RUN dotnet build "RPGCM.API.csproj" -c Release -o /app/build

# Publicar
RUN dotnet publish "RPGCM.API.csproj" -c Release -o /app/publish

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RPGCM.API.dll"]
