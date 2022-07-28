FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server/src/Startup/Startup.csproj", "Server/src/Startup/"]
RUN dotnet restore "Server/src/Startup/Startup.csproj"
COPY . .
WORKDIR /src/Server/src/Startup
RUN dotnet build "Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookingRecipesSystem.Startup.dll"]
