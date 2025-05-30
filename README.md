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

# Linguagem Ubíqua

- **Totem**: Terminal de Atendimento automatizado com o qual o cliente irá interagir, no modelo Wizard;
  - **Modelo Wizard**: Processo passo a passo que permitirá ao usuário escolher os itens do lanche por etapas e por tipo;
- **Cliente Identificado**: Cliente que informou seu CPF para prosseguir com o pedido;
- **Cliente Cadastrado**: Cliente que se cadastrou (Nome, Email e CPF) para receber conteúdos via email.

- **Pedido**: Agrupamento de um ou mais itens escolhidos pelo cliente para consumo;
- **Ingredientes**: Itens responsáveis para preparação de um Produto do Pedido.

- **Produto**: Itens que o cliente pode selecionar na montagem do Pedido;
  - **Lanche**: Primeiro tipo de Produto do Pedido que pode ser selecionado (um ou mais). É possível prosseguir com o pedido sem o Lanche;
  - **Acompanhamento**: Segundo tipo de Produto do Pedido que pode ser selecionado (um ou mais). É possível prosseguir com o pedido sem o Acompanhamento;
  - **Bebida**: Terceiro tipo de Produto do Pedido que pode ser selecionado (uma ou mais). É possível prosseguir com o pedido sem a Bebida;
  - **Sobremesa**: Quarto tipo de Produto do Pedido que pode ser selecionada (uma ou mais).

- **Status do Pedido**
  - **Recebido**: Pedido que teve seu pagamento efetuado e enviado para a cozinha;
  - **Em preparação**: Pedido que, após ter os ingredientes validados, foi iniciado o preparo;
  - **Pronto**: Pedido que teve seu preparo finalizado e está pronto para entrega ao cliente;
  - **Finalizado**: Pedido entregue ao cliente com sucesso.

- **Pagamento**: Operação efetivar a compra do Pedido;
- **QR Code**: Imagem a ser gerada pelo Totem para permitir o pagamento via PIX;
- **[SE] Mercado Pago**: Gateway de pagamento selecionado para a integração;
- **Comprovante**: Papel impresso pelo Totem ao final do pedido contendo um identificador amigavel para acompanhamento do andamento do pedido.

- **Cozinha**: Área responsável por efetuar a preparação do Pedido;
- **Balcão**: Área responsável por realizar a entrega do Pedido para o Cliente;
- **Telão**: Visualização que permitirá ao cliente ver a fila de Pedidos sendo Preparados e que estão Prontos;

# Tecnologias utilizadas
- **Linguagem**: C# / Asp.Net Core Web Api (dotnet 8)
- **Banco de Dados**: SQL Server 2022
- **ORM**: Entity Framework Core
  - **Migration** sendo aplicada no startup da aplicação
- **Controle de Versão**: GitHub

Escolhas efetuadas de acordo com a familiariade de todo o grupo para acelerarmos o tempo de desenvolvimento e focarmos nas atividades fundamentais

# Migration
Da raíz do projeto

```shell
dotnet ef migrations add --project src/Adapters/Driven/Infra.Database.SqlServer/Infra.Database.SqlServer.csproj --startup-project src/Adapters/Driving/TechChallengeFastFood.API/TechChallengeFastFood.API.csproj --context Infra.Database.SqlServer.AppDbContext --configuration Debug <MIGRATION_NAME> --output-dir Migrations
```

Alterar o <MIGRATION_NAME> por um nome significativo