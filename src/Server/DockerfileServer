#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/src"
COPY ["Server/Startup/Startup.csproj", "Server/Startup/"]
RUN dotnet restore "Server/Startup/Startup.csproj"
COPY . .
WORKDIR "/src/Server/Startup"
RUN dotnet build "Startup.csproj" -c Release -o /app/server/build


FROM build AS publish
RUN dotnet publish "Startup.csproj" -c Release -o /app/server/publish


FROM base AS final
WORKDIR "/app/server"
COPY --from=publish /app/server/publish .
ENTRYPOINT ["dotnet", "Startup.dll"]