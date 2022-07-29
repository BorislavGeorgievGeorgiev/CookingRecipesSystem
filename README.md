# CookingRecipesSystem

### **Try it.**

  You must have already installed [Docker Desktop](https://docs.docker.com/docker-for-windows/install/).

  ```
  git clone https://github.com/BorislavGeorgievGeorgiev/CookingRecipesSystem.git
  cd CookingRecipesSystem
  docker-compose -f "docker-compose.debug.yml" up --build -d
  ```

  <https://localhost:6001/swagger>
  
***Note 1 :***
  Because of possible "Compiler Error CS5001" when we use docker with "dotnet/sdk:6.0", must chose "context" for "build" to be the folder of "sln" file or parent folder. Also path to "dockerfile" and path to "csproj" files must start with the child folder of "context". The paths are case sensitive.

***Note 2 :***
  To prevent "nginx: [emerg] unknown directive" error,  "nginx.conf" file must be with "UTF-8" encoding and "LF" end of line(EOL).

***Note 3 :***
  Setup docker ssl:<https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0>
  
  ```
  dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\CookingRecipesSystem.pfx -p MyStrong(!)Password123
  dotnet dev-certs https --trust
  ```
