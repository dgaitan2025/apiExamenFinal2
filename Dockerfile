# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos del proyecto y restaura dependencias
COPY *.sln .
COPY apiExamenFinal/*.csproj apiExamenFinal/
RUN dotnet restore

# Copia todo el código y compila
COPY . .
WORKDIR /src/apiExamenFinal
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "apiExamenFinal.dll"]
