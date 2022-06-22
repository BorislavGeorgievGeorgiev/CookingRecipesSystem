FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Client/src/Startup/Startup.csproj", "Client/src/Startup/"]
RUN dotnet restore "Client/src/Startup/Startup.csproj"
COPY . .
WORKDIR /src/Client/src/Startup
RUN dotnet build "Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Startup.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
RUN rm -rf /usr/share/nginx/html/*
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["Client/src/Startup/nginx.conf", "/etc/nginx/nginx.conf"]
