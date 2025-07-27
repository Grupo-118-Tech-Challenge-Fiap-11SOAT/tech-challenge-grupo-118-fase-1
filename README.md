# Tech Challenge - Fast Food API - Fase 2 - Grupo 118

# Membros do Grupo
- Sabrina Cardoso de Oliveira
  - **Matrícula**: RM363507
  - **Usuário Discord**: sah.mdo
- Tiago Cristiano Koch
  - **Matrícula**: RM361415
  - **Usuário Discord**: tiagokoch0076
- Tiago Victor de Oliveira
  - **Matrícula**: RM364588
  - **Usuário Discord**: oliveirad.tiago
- Túlio Henrique de Paula Rezende
  - **Matrícula**: RM360982
  - **Usuário Discord**: tuliomamute
- Vinícius Rossmann Nunes
  - **Matrícula**: RM362963
  - **Usuário Discord**: _viniciusnunes

# Tecnologias utilizadas
- **Linguagem**: C# / Asp.Net Core Web Api (dotnet 8)
- **Banco de Dados**: SQL Server 2022
- **ORM**: Entity Framework Core
  - **Migration** sendo aplicada no startup da aplicação
- **Controle de Versão**: GitHub
- **Kubernetes**
  - **Helm**: Utilizado para facilitar o deploy da aplicação e do banco de dados
  - **Docker**: Utilizado para containerização da aplicação e do banco de dados
  - **Docker Desktop/Rancher Desktop**: Utilizados para simular o ambiente de produção localmente

Escolhas efetuadas de acordo com a familiariade de todo o grupo para acelerarmos o tempo de desenvolvimento e focarmos nas atividades fundamentais

Com isso deve ser gerado um token que deve ser fornecido no canto superior direito clicando no botão Authorize, adicionar a palavra Bearer e colar o conteúdo da resposta do endpoint Login, Ex: Bearer eyJhbGciOiJIUzI1NiIsI e clicar em Authorize. Após isso fechar clicar em close e seu usuário estará autenticado e com acesso a todos os endpoints da aplicação.

# Vídeo de Demonstração
Para facilitar a visualização do funcionamento da API, foi criado um vídeo de demonstração que mostra o fluxo de criação de pedidos, incluindo a autenticação e o uso do Mercado Pago para pagamento via Pix. Para assistir, clique na imagem abaixo:

[![Watch the video](11SOAT%20-%20Fase%202%20-%20Grupo%20118.jpeg)](https://www.youtube.com/watch?v=VIDEO_ID)

# Collection do Postman
Para facilitar o teste dos endpoints da API, foi criada uma que contém todos os endpoints disponíveis, incluindo os de autenticação e os de criação de pedidos.

[![Run in Postman](https://run.pstmn.io/button.svg)]()

# Helm - SQL Server

Para facilitar a instalação do SQL Server em um cluster Kubernetes, foi criado um [chart Helm](https://helm.sh/docs/intro/install/). O chart inclui as configurações necessárias para o banco de dados, como usuário, senha e porta.
Comando de instalação (a partir da pasta raiz do projeto)
```shell
helm upgrade --install sqlserver infra/helm/devsqlchart
```

Dados para acesso ao banco de dados

- **Usuário**: sa
- **Senha**: Mssql!Passw0rd
- **Host**: localhost, 31390 (porta do serviço NodePort)

Connection string de exemplo:
```plaintext
Data Source=localhost,31390;Database=TechChallengeFastFoodFase2;Integrated Security=false;User ID=sa;Password=Mssql!Passw0rd;TrustServerCertificate=true;Max Pool Size=200;Min Pool Size=10;Connection Timeout=30;
```

## Estrutura do Banco de Dados

Abaixo segue a modelagem do nosso banco de dados, contendo como as entidades se relacionam.

![Diagrama do Banco de Dados](TechChallengeFastFoodDatabaseDiagram-V2.png)

# Helm - API

## Utilizando a autenticação
Inicialmente temos um usuário admin cadastrado, para validar as credenciais acesse no swagger o endpoint de Login, fornecendo as credenciais
```
email: admin@admin.com
password:  adminPass
```

## Configuração
Para geração do qr code com o mercado pago, estão sendo utilizadas credenciais de sandbox. Para utilizar sua própria conta, basta atualizar
as variáveis de ambiemte com o prefixo "MercadoPago__" no docker-compose.yaml.

Por ainda não ser possível confirmar um pagamento pix em ambiente de sandbox, a simulação de confirmação pode ser feita através do endpoint /payment/{id}, informando o código do pagamento a ser confirmado.

## Build da Imagem (da raiz do projeto)

```bash
docker build . -f src/CleanArchitecture/Infrastructure/TechChallengeFastFood.CleanArch.Infrastructure.API/Dockerfile -t fiaptechchallengelocal/techchallengefastfoodapi118fase2:latest -t fiaptechchallengelocal/techchallengefastfoodapi118fase2:1.0.0
```

## Deploy (da raiz do projeto)

```bash
helm upgrade --install techchallengefastfoodapi118fase2 infra/helm/techchallengefastfoodapi118fase2
```

## Acessando a API
Se o deploy foi realizado com sucesso, a API estará acessível através do endereço: http://localhost:30080

# Diagramas
## Aplicação
### Diagrama de fluxo de dados
```mermaid
%% Objects Definition
flowchart TD

  User

%% Infrastructure Layer
API(🔵Handler)
Repository(🔵Database Access)
Database[(🔵Database created with Entity Framework)]

%%Presentation Layer
Controller[🟢Clean Architecture Controller]
Gateway[🟢Clean Architecture Gateway]
Presenter[🟢Clean Architecture Presenter]

%% Application
UseCase[🔴Clean Architecture UseCase]

%% Domain Layer
Entity[🟡Clean Architecture Domain]

%% Data inbound 
User ---> | 1-HTTP Request | API
API ---> | 2-Parse/Validate | Controller
Controller ----> | 3-Call | UseCase
UseCase ---> | 4-Validate Inputs | Entity
UseCase -----> | 5-Call | Gateway
Gateway ---> | 6-Create Database Entity | Repository
Repository ---> | 7-Using EF Core | Database

%% Data outbound
Repository ---> | 8-Returns values | Gateway
Gateway ---> | 9-Convert DB Entity to Domain Entity | UseCase
UseCase ---> | 10-Returns | Controller
Controller ---> | 11-Call | Presenter
Presenter ---> | 12-Creates Output Result | Controller
Controller ---> | 13-Return Data | API
API ---> | 14-Return Data | User
```

### Diagrama de sequência - Pedido
```mermaid
sequenceDiagram
    actor Client
    participant APIOrderController
    
    participant CleanOrderController

    participant GetActiveProductsByIdsUseCase
    participant ProductGateway
    participant ProductRepository

    participant OrderGateway
    participant OrderRepository
    participant OrderPresenter
    participant OrderItemPresenter

    participant CreateOrderUseCase


    participant Order
    participant Database

    %% Request from client to API
    Client ->> APIOrderController: HTTP POST
    APIOrderController ->> CleanOrderController: DataConversion and <br/>Validation
    
    %% Clean Order Controller Logic
    CleanOrderController ->> GetActiveProductsByIdsUseCase: Search ActiveProducts
    GetActiveProductsByIdsUseCase ->> ProductGateway: Search ActiveProducts
    ProductGateway ->> ProductRepository: Search ActiveProducts
    ProductRepository ->> Database: Get Products Elements

    %% Search Active Products
    Database -->> ProductRepository: List<Product>
    ProductRepository -->> ProductGateway: List<Product>
    ProductGateway -->> GetActiveProductsByIdsUseCase: List<Product>
    GetActiveProductsByIdsUseCase -->> CleanOrderController: List<Product>

    %% Create Order
    CleanOrderController ->> CreateOrderUseCase: Create Order Request
    CreateOrderUseCase ->> Order: Create Order Entity
    CreateOrderUseCase ->> OrderGateway: Call Gateway to Persist
    OrderGateway ->> OrderRepository: Persist Data
    OrderRepository ->> Database: Create Order Entry

    %% Return Persisted Data
    OrderRepository -->> OrderGateway: Return Created Order
    OrderGateway -->> CreateOrderUseCase: Return Domain Entity Order
    CreateOrderUseCase -->> CleanOrderController: Return OrderGateway

    %% Send Data to Presenter
    CleanOrderController -> OrderPresenter: Send Order Entity to Convert
    OrderPresenter -> OrderItemPresenter: Convert elements to items

    OrderItemPresenter -->> OrderPresenter: Return items
    OrderPresenter -->> CleanOrderController: Return converted items

    %% Send Data to API
    CleanOrderController -->> APIOrderController: Create Order result data
    APIOrderController -->> Client: Return data to client
```

### Diagrama de sequência - Pagamento
```mermaid
sequenceDiagram
    actor Client
    participant PaymentController
    participant GetOrderByIdUseCase
    participant OrderGateway
    participant OrderRepository

    participant CreatePaymentUseCase
    participant Payment
    participant PaymentGateway
    participant PaymentRepository
    participant PaymentProcessorFactory
    participant PaymentProcessor
    participant PaymentPresenter

    participant GetPaymentByIdUseCase
    participant ConfirmPaymentUseCase
    participant ConfirmOrderUseCase

    %% Create Payment Flow
    Client ->> PaymentController: CreatePaymentAsync(PaymentRequest)
    PaymentController ->> GetOrderByIdUseCase: ExecuteAsync(OrderId)
    GetOrderByIdUseCase ->> OrderGateway: GetByIdAsync
    OrderGateway ->> OrderRepository: GetByIdAsync
    OrderRepository -->> OrderGateway: Order
    OrderGateway -->> GetOrderByIdUseCase: Order
    GetOrderByIdUseCase -->> PaymentController: Order

    PaymentController ->> CreatePaymentUseCase: ExecuteAsync(Order, PaymentRequest)
    CreatePaymentUseCase ->> Payment: new Payment(...)
    CreatePaymentUseCase ->> PaymentGateway: ProcessPaymentAsync(Payment)
    PaymentGateway ->> PaymentProcessorFactory: GetProcessor
    PaymentProcessorFactory -->> PaymentGateway: PaymentProcessor
    PaymentGateway ->> PaymentProcessor: ProcessAsync
    PaymentProcessor -->> PaymentGateway: ProcessedPaymentDto
    PaymentGateway -->> CreatePaymentUseCase: ProcessedPaymentDto
    CreatePaymentUseCase ->> PaymentGateway: CreatePaymentAsync
    PaymentGateway ->> PaymentRepository: CreateAsync
    PaymentRepository -->> PaymentGateway: Persisted PaymentEntity
    PaymentGateway -->> CreatePaymentUseCase: Payment
    CreatePaymentUseCase -->> PaymentController: Payment

    PaymentController ->> PaymentPresenter: Convert(Payment)
    PaymentPresenter -->> PaymentController: PaymentResponse
    PaymentController -->> Client: Return PaymentResponse

    %% Confirm Payment Flow
    Client ->> PaymentController: ConfirmPaymentAsync(PaymentId)
    PaymentController ->> GetPaymentByIdUseCase: ExecuteAsync(id)
    GetPaymentByIdUseCase ->> PaymentGateway: GetPaymentByIdAsync
    PaymentGateway ->> PaymentRepository: GetByIdAsync
    PaymentRepository -->> PaymentGateway: PaymentEntity
    PaymentGateway -->> GetPaymentByIdUseCase: Domain Payment
    GetPaymentByIdUseCase -->> PaymentController: Payment

    PaymentController ->> GetOrderByIdUseCase: ExecuteAsync(OrderId)
    GetOrderByIdUseCase ->> OrderGateway: GetByIdAsync
    OrderGateway ->> OrderRepository: GetByIdAsync
    OrderRepository -->> OrderGateway: Order
    OrderGateway -->> GetOrderByIdUseCase: Order
    GetOrderByIdUseCase -->> PaymentController: Order

    PaymentController ->> ConfirmPaymentUseCase: ExecuteAsync(Payment, Order)
    ConfirmPaymentUseCase ->> Payment: SetStatusToApproved
    ConfirmPaymentUseCase ->> PaymentGateway: ConfirmPaymentAsync
    PaymentGateway ->> PaymentRepository: UpdateAsync
    PaymentRepository -->> PaymentGateway: void
    ConfirmPaymentUseCase ->> ConfirmOrderUseCase: ExecuteAsync(Order)
    ConfirmOrderUseCase ->> OrderGateway: UpdateAsync
    OrderGateway ->> OrderRepository: UpdateAsync
    OrderRepository -->> OrderGateway: void
    OrderGateway -->> ConfirmOrderUseCase: void
    ConfirmOrderUseCase -->> ConfirmPaymentUseCase: void
    ConfirmPaymentUseCase -->> PaymentController: Updated Payment

    PaymentController ->> PaymentPresenter: Convert(Payment)
    PaymentPresenter -->> PaymentController: PaymentResponse
    PaymentController -->> Client: Return PaymentResponse
```

## Kubernetes

### Arquitetura do Cluster - Cenário Local

![Arquitetura do Cluster Local](1%20-%20Kubernetes%20Architecture%20-%20Tech%20Challenge%20-%20Abordagem%20Local.drawio.png)

### Arquitetura do Cluster - Cenário Produção

![Arquitetura do Cluster Produção](2%20-%20Kubernetes%20Architecture%20-%20Tech%20Challenge%20-%20Abordagem%20_Produtiva_.drawio.png)