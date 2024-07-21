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
        var produtos = new List<Produto>();

        using (var connection = CreateConnection())
        {
            await connection.OpenAsync();

            using (var command = new NpgsqlCommand("SELECT * FROM produtos", connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    produtos.Add(new Produto
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Tipo = (TipoProduto)reader.GetInt32(2),
                        PrecoUnitario = reader.GetDecimal(3),  // Sem alteração necessária aqui
                    });
                }
            }
        }

        return produtos;
    }

    public async Task<Produto> GetByIdAsync(int id)
    {
        Produto produto = null;

        using (var connection = CreateConnection())
        {
            await connection.OpenAsync();

            using (var command = new NpgsqlCommand("SELECT * FROM produtos WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        produto = new Produto
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Tipo = (TipoProduto)reader.GetInt32(2),
                            PrecoUnitario = reader.GetDecimal(3)  // Sem alteração necessária aqui
                        };
                    }
                }
            }
        }

        return produto;
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
                command.Parameters.AddWithValue("Preco_Unitario", produto.PrecoUnitario);  // Nome corrigido

                var id = (int)await command.ExecuteScalarAsync();
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
                command.Parameters.AddWithValue("Preco_Unitario", produto.PrecoUnitario);  // Nome corrigido
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
