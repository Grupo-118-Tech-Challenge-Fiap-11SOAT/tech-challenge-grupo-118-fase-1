FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.Infrastructure.API/TechChallengeFastFood.CleanArch.Infrastructure.API.csproj", "src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.Infrastructure.API/"]
COPY ["src/CleanArchitecture/Presentation/Controllers/Controllers.csproj", "src/CleanArchitecture/Presentation/Controllers/"]
COPY ["src/CleanArchitecture/Application/UseCases/UseCases.csproj", "src/CleanArchitecture/Application/UseCases/"]
COPY ["src/CleanArchitecture/Common/Common.Dto/Common.Dto.csproj", "src/CleanArchitecture/Common/Common.Dto/"]
COPY ["src/CleanArchitecture/Common/Common.Interfaces/Common.Interfaces.csproj", "src/CleanArchitecture/Common/Common.Interfaces/"]
COPY ["src/CleanArchitecture/Domain/Entities/Entities.csproj", "src/CleanArchitecture/Domain/Entities/"]
COPY ["src/CleanArchitecture/Presentation/Gateway/Gateway.csproj", "src/CleanArchitecture/Presentation/Gateway/"]
COPY ["src/CleanArchitecture/Presentation/Presenters/Presenters.csproj", "src/CleanArchitecture/Presentation/Presenters/"]
COPY ["src/CleanArchitecture/Infrastructure/Database.SqlServer/Database.SqlServer.csproj", "src/CleanArchitecture/Infrastructure/Database.SqlServer/"]

RUN dotnet restore "src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.Infrastructure.API/TechChallengeFastFood.CleanArch.Infrastructure.API.csproj"

COPY . .

WORKDIR "/src/src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.Infrastructure.API"
RUN dotnet build "./TechChallengeFastFood.CleanArch.Infrastructure.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallengeFastFood.CleanArch.Infrastructure.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

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

ENTRYPOINT ["dotnet", "TechChallengeFastFood.CleanArch.Infrastructure.API.dll"]
