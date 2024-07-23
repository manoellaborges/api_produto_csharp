# ApiProduto

Este é um projeto de uma API para gerenciamento de produtos, criado para fins de estudo. Foi desenvolvido na linguagem C# e está seguindo os padrões RESTful e dois dos princípios SOLID: SRP e DIP. Utiliza o protocolo Basic e os dados estão sendo armazenados no PostgreSQL.

## Índice

- [Pré-requisitos](#pré-requisitos)
- [Instalação](#instalação)
- [Instanciar o Banco de Dados](#instanciar-o-banco-de-dados)
- [Executar a Aplicação](#executar-a-aplicação)
- [Testar a API](#testar-a-api)
- [Endpoints](#endpoints)
  - [Produtos](#produtos)
  - [Dashboard](#dashboard)
- [Perguntas](#perguntas)
  - [Quais princípios SOLID foram usados? Qual foi o motivo da escolha deles?](#quais-princípios-solid-foram-usados-qual-foi-o-motivo-da-escolha-deles)
  - [Dado um cenário que necessite de alta performance, cite 2 locais possíveis de melhorar a performance da API criada e explique como seria a implementação desta melhoria.](#dado-um-cenário-que-necessite-de-alta-performance-cite-2-locais-possíveis-de-melhorar-a-performance-da-api-criada-e-explique-como-seria-a-implementação-desta-melhoria)

## Pré-requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Postman](https://www.postman.com/downloads/)

## Instalação

1. Clone o repositório:

    ```bash
    git clone https://github.com/seu-usuario/ApiProduto.git
    cd ApiProduto
    ```

2. Configure a string de conexão do banco de dados no arquivo `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=nome_do_banco;Username=seu_usuario;Password=sua_senha"
      }
    }
    ```

3. Instale as dependências do projeto:

    ```bash
    dotnet restore
    ```

## Instanciar o Banco de Dados

1. Conecte-se ao PostgreSQL e crie o banco de dados:

    ```sql
    CREATE DATABASE nome_do_banco;
    ```

2. Crie a tabela `Produtos`:

    ```sql
    create table produtos (
    id SERIAL primary key,
    nome VARCHAR(255) not null,
    tipo INTEGER not null,
    preco_unitario DECIMAL not NULL
    );
    ```

## Executar a Aplicação

1. Compile e execute a aplicação:

    ```bash
    dotnet run
    ```

2. A aplicação estará disponível em [https://localhost:7112](https://localhost:7112).

## Testar a API

1. **Usando o Swagger UI**:

    Abra o navegador e acesse [https://localhost:7112/swagger](https://localhost:7112/swagger) para acessar a documentação e testar os endpoints da API.

2. **Usando o Postman**:

    - Crie uma nova requisição `GET` para `https://localhost:7112/api/Produtos`.
    - Adicione autenticação básica com o `username` `admin` e `password` `password`.
    - Envie a requisição e veja a resposta.

## Endpoints

### Produtos

- `GET /api/Produtos`: Retorna todos os produtos.
- `GET /api/Produtos/{id}`: Retorna um produto específico pelo ID.
- `POST /api/Produtos`: Cria um novo produto.
- `PUT /api/Produtos/{id}`: Atualiza um produto existente pelo ID.
- `DELETE /api/Produtos/{id}`: Deleta um produto específico pelo ID.

### Dashboard

- `GET /api/dashboard`: Retorna a quantidade e o preço unitário médio segregado por tipo de produto.

## Perguntas

### Quais princípios SOLID foram usados? Qual foi o motivo da escolha deles?

O primeiro princípio, SRP (Single Responsibility Principle), foi utilizado para garantir uma estrutura bem definida na API. Em cada classe, é implementado apenas o que é necessário para cumprir seu objetivo, facilitando a manutenção do código. Por exemplo, na API de Produto, o princípio SRP é aplicado nos controladores, que se concentram apenas em gerenciar as requisições HTTP e interagir com os serviços ou repositórios necessários, delegando responsabilidades específicas para as classes de serviços e repositórios.

O segundo princípio, DIP (Dependency Inversion Principle), destaca a importância das abstrações, ou seja, o uso de interfaces, tornando a API mais flexível a mudanças. Neste projeto, o uso de interfaces e injeção de dependência é evidente, permitindo que o controlador 'ProdutosController' dependa de abstrações como 'IProdutoRepository' em vez de depender diretamente das implementações concretas como 'ProdutoRepository'.

### Dado um cenário que necessite de alta performance, cite 2 locais possíveis de melhorar a performance da API criada e explique como seria a implementação desta melhoria.

Um quesito importante para uma boa performance da API e que inclusive foi utilizado nesse projeto, foi a programação assíncrona. A sua melhoria de desempenho permite uma alta escalabilidade fazendo com sistema suporte um grande número de requisições e que o servidor lide com elas de forma simultânea, sem bloquear a thread principal enquanto ela espera por operações de acesso ao banco de dados;

Outros dois pontos que podem melhorar ainda mais a performance são o balanceamento de carga e as partições no banco de dados. O balanceamento de carga é crucial quando há muitas requisições, pois evita a sobrecarga e a queda da API. Utilizando o algoritmo Round-Robin, a distribuição das requisições é feita de forma igualitária entre todos os componentes do servidor, garantindo um processamento equilibrado.

Já o particionamento do banco de dados é útil para tabelas com muitos registros. Ao dividir esses registros em subtabelas, criam-se partições que limitam o campo de pesquisa, facilitando o gerenciamento e aumentando o desempenho da base de dados.
