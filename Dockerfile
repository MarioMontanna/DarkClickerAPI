# Etapa 1: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar los archivos del proyecto
COPY . ./

# Publicar la aplicación
RUN dotnet publish -c Release -o /out

# Etapa 2: Imagen para ejecución (aspnet SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# Copiar los archivos publicados desde la etapa de construcción
COPY --from=build /out ./

# Copiar el archivo SQLite desde el directorio de publicación
COPY --from=build /app/bin/Release/net8.0/app.sqlite /app/app.sqlite

# Exponer el puerto que usará la aplicación
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "DarkClicker.dll"]
#Hay que copiar el app.sqlite a mano con en el csproj
# <ItemGroup>
  #<None Update="app.sqlite">
  #<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
#</None>
#</ItemGroup>