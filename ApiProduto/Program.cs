using ApiProduto.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ApiProduto.Services;
using ApiProduto.Authentication;

namespace ApiProduto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicionar servi�os
            builder.Services.AddControllers();

            // Configurar autentica��o b�sica
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


            // Adicionar o servi�o ProdutoRepository e ProdutoService
            builder.Services.AddScoped<IProdutoRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new ProdutoRepository(connectionString);
            });
            builder.Services.AddScoped<IProdutoService, ProdutoService>();

            // Adicionar o Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configura��o do pipeline de requisi��es
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();


        }
    }

}