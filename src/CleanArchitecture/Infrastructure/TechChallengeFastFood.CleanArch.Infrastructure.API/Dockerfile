FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.API/TechChallengeFastFood.CleanArch.API.csproj", "src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.API/"]
RUN dotnet restore "src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.API/TechChallengeFastFood.CleanArch.API.csproj"
COPY . .
WORKDIR "/src/src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.API"
RUN dotnet build "./TechChallengeFastFood.CleanArch.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallengeFastFood.CleanArch.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallengeFastFood.CleanArch.API.dll"]
