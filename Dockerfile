# Etapa 1 – Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiem solution și proiectele
COPY *.sln .
COPY SmartGreenhouseControlSystem.API/ SmartGreenhouseControlSystem.API/
COPY SmartGreenhouseControlSystem.Application/ SmartGreenhouseControlSystem.Application/
COPY SmartGreenhouseControlSystem.Domain/ SmartGreenhouseControlSystem.Domain/
COPY SmartGreenhouseControlSystem.Infrastructure/ SmartGreenhouseControlSystem.Infrastructure/

WORKDIR /app/SmartGreenhouseControlSystem.API
RUN dotnet restore
RUN dotnet publish -c Release -o /publish

# Etapa 2 – Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartGreenhouseControlSystem.API.dll"]