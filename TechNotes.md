
# Migration
Da raíz do projeto

```shell
dotnet ef migrations add --project src/Adapters/Driven/Infra.Database.SqlServer/Infra.Database.SqlServer.csproj --startup-project src/Adapters/Driving/TechChallengeFastFood.API/TechChallengeFastFood.API.csproj --context Infra.Database.SqlServer.AppDbContext --configuration Debug <MIGRATION_NAME> --output-dir Migrations
```

Alterar o <MIGRATION_NAME> por um nome significativo