using ApiProduto.Models;
using ApiProduto.Repositories;
using Npgsql;

public class ProdutoRepository : IProdutoRepository
{
    private readonly string _connectionString;

    public ProdutoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        List<Produto> produtos = new();

        var connection = CreateConnection();

        await connection.OpenAsync();

        NpgsqlCommand command = new("SELECT * FROM produtos", connection);
        NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            produtos.Add(new Produto
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Tipo = (TipoProduto)reader.GetInt32(2),
                PrecoUnitario = reader.GetDecimal(3)
            });
        }

        return produtos;
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        NpgsqlConnection connection = CreateConnection();

        await connection.OpenAsync();

        NpgsqlCommand command = new("SELECT * FROM produtos WHERE Id = @Id", connection);

        command.Parameters.AddWithValue("Id", id);
        NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ?
            new Produto
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Tipo = (TipoProduto)reader.GetInt32(2),
                PrecoUnitario = reader.GetDecimal(3)
            } : null;
    }

    public async Task AddAsync(Produto produto)
    {
        using (var connection = CreateConnection())
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("INSERT INTO produtos (Nome, Tipo, Preco_Unitario) VALUES (@Nome, @Tipo, @Preco_Unitario) RETURNING Id", connection))
            {
                command.Parameters.AddWithValue("Nome", produto.Nome);
                command.Parameters.AddWithValue("Tipo", (int)produto.Tipo);
                command.Parameters.AddWithValue("Preco_Unitario", produto.PrecoUnitario);

                int id = (int)await command.ExecuteScalarAsync();
                produto.Id = id;
            }
        }
    }

    public async Task UpdateAsync(Produto produto)
    {
        using (var connection = CreateConnection())
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("UPDATE produtos SET Nome = @Nome, Tipo = @Tipo, Preco_Unitario = @Preco_Unitario WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("Nome", produto.Nome);
                command.Parameters.AddWithValue("Tipo", (int)produto.Tipo);
                command.Parameters.AddWithValue("Preco_Unitario", produto.PrecoUnitario);
                command.Parameters.AddWithValue("Id", produto.Id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = CreateConnection())
        {
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand("DELETE FROM produtos WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
