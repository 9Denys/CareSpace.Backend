# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CareSpace.Backend.API/CareSpace.Backend.API.csproj", "CareSpace.Backend.API/"]
COPY ["CareSpace.Backend.Application/CareSpace.Backend.Application.csproj", "CareSpace.Backend.Application/"]
COPY ["CareSpace.Backend.Contracts/CareSpace.Backend.Contracts.csproj", "CareSpace.Backend.Contracts/"]
COPY ["CareSpace.Backend.Domain/CareSpace.Backend.Domain.csproj", "CareSpace.Backend.Domain/"]
COPY ["CareSpace.Backend.Infrastructure/CareSpace.Backend.Infrastructure.csproj", "CareSpace.Backend.Infrastructure/"]
RUN dotnet restore "./CareSpace.Backend.API/CareSpace.Backend.API.csproj"
COPY . .
WORKDIR "/src/CareSpace.Backend.API"
RUN dotnet build "./CareSpace.Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CareSpace.Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CareSpace.Backend.API.dll"]