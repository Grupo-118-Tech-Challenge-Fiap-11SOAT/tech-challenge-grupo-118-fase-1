# Tech Challenge - Fast Food API - Grupo 118

[Projeto base para estudos](https://github.com/tuliorezende/practice-hexagon-architecture)

# Membros do Grupo
- Sabrina Cardoso
- Tiago Koch
- Tiago Oliveira
- Túlio Rezende
- Vinícius Nunes

# Miro
- [Clique aqui para acessar o Miro do Projeto](https://miro.com/app/board/uXjVIDgIqTM=/?share_link_id=224738363095)

# Notion
- [Clique aqui para acessar o Notion do Projeto](https://www.notion.so/1d25c6f7a32e8045949dd2c342b6a403?v=1d25c6f7a32e806bb165000c707b9a3d&pvs=4)

# Migration
Da raíz do projeto

```shell
dotnet ef migrations add --project src/Adapters/Driven/Infra.Database.SqlServer/Infra.Database.SqlServer.csproj --startup-project src/Adapters/Driving/TechChallengeFastFood.API/TechChallengeFastFood.API.csproj --context Infra.Database.SqlServer.AppDbContext --configuration Debug <MIGRATION_NAME> --output-dir Migrations
```

Alterar o <MIGRATION_NAME> por um nome significativo