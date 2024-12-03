# Building app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restoring dependencies
COPY *.csproj ./
RUN dotnet restore

# Copying proyect files
COPY . ./

# Publising la aplication
RUN dotnet publish -c Release -o /out

# Etapa 2: Image para execution (aspnet SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
# Copying filer published from the build
COPY --from=build /out ./

# Sqlite directory
COPY --from=build /app/bin/Release/net8.0/app.sqlite /app/app.sqlite

# Aplication port
EXPOSE 80

# Comand to execute aplication
ENTRYPOINT ["dotnet", "DarkClicker.dll"]
# You must copy mannualy the database, the app.sqlite file in the csproj
# <ItemGroup>
  #<None Update="app.sqlite">
  #<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
#</None>
#</ItemGroup>