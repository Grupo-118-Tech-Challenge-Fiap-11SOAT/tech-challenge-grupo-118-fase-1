FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Adapters/Driving/TechChallengeFastFood.API/TechChallengeFastFood.API.csproj", "src/Adapters/Driving/TechChallengeFastFood.API/"]

COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]

COPY ["src/Adapters/Driven/Infra.Database.Memory/Infra.Database.Memory.csproj", "src/Adapters/Driven/Infra.Database.Memory/"]
COPY ["src/Adapters/Driven/Infra.Database.SqlServer/Infra.Database.SqlServer.csproj", "src/Adapters/Driven/Infra.Database.SqlServer/"]

COPY ["src/Adapters/Driven/Infra.Api.Memory/Infra.Api.Memory.csproj", "src/Adapters/Driven/Infra.Api.Memory/"]
COPY ["src/Adapters/Driven/Infra.Api.MercadoPago/Infra.Api.MercadoPago.csproj", "src/Adapters/Driven/Infra.Api.MercadoPago/"]

RUN dotnet restore "src/Adapters/Driving/TechChallengeFastFood.API/TechChallengeFastFood.API.csproj"
COPY . .
WORKDIR "/src/src/Adapters/Driving/TechChallengeFastFood.API"
RUN dotnet build "./TechChallengeFastFood.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallengeFastFood.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

ENV \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8

RUN apk add --no-cache \
    icu-data-full \
    icu-libs \
    curl

COPY --from=publish /app/publish .

USER $APP_UID

ENTRYPOINT ["dotnet", "TechChallengeFastFood.API.dll"]
