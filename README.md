# Tech Challenge - Fast Food API - Grupo 118

[Projeto base para estudos](https://github.com/tuliorezende/practice-hexagon-architecture)

# Membros do Grupo
- TODO: Inserir membros do grupo

# Miro
- TODO: Inserir Link do Miro

# Notion
- TODO: Inserir Link do Notion


# Migration
Da raíz do projeto

```shell
dotnet ef migrations add --project src/Adapters/Driven/Infra.Database.SqlServer/Infra.Database.SqlServer.csproj --startup-project src/Adapters/Driving/TechChallengeFastFood.API/TechChallengeFastFood.API.csproj --context Infra.Database.SqlServer.AppDbContext --configuration Debug <MIGRATION_NAME> --output-dir Migrations
```

Alterar o <MIGRATION_NAME> por um nome significativo