FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server/Startup/Startup.csproj", "Server/Startup/"]
RUN dotnet restore "Server/Startup/Startup.csproj"
COPY . .
WORKDIR /src/Server/Startup
RUN dotnet build "Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Startup.dll"]