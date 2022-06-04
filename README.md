# CookingRecipesSystem

***Note 1 :***
  Because of possible "Compiler Error CS5001" when we use docker with "dotnet/sdk:6.0", must chose "context" for "build" to be the folder of "sln" file or parent folder. Also path to "dockerfile" and path to "csproj" files must start with the child folder of "context". The paths are case sensitive.

***Note 2 :***
  To prevent "nginx: [emerg] unknown directive" error,  "nginx.conf" file must be with "UTF-8" encoding and "LF" end of line(EOL).
