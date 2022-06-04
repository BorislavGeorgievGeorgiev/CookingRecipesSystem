FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Client/Startup/Startup.csproj", "Client/Startup/"]
RUN dotnet restore "Client/Startup/Startup.csproj"
COPY . .
WORKDIR /src/Client/Startup
RUN dotnet build "Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Startup.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
RUN rm -rf /usr/share/nginx/html/*
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["Client/Startup/nginx.conf", "/etc/nginx/nginx.conf"]
