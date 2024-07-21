# ApiProduto

Este é um projeto de uma API para gerenciamento de produtos, criado para fins de estudo. Foi desenvolvido na linguagem C# e está seguindo os padrões RESTful e dois dos princípios SOLID: SRP e DIP. Utiliza o protocolo Basic e os dados estão sendo armazenados no PostgreSQL.

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

2. A aplicação estará disponível em `[https://localhost:7112]`.

## Testar a API

1. **Usando o Swagger UI**:

    Abra o navegador e acesse `[https://localhost:7112/swagger]` para acessar a documentação e testar os endpoints da API.

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
