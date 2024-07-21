using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProduto.Models
{
    public enum TipoProduto
    {
        Material,
        Servico
    }

    [Table("Produtos")]
    public class Produto
    {
        
        public int Id { get; set; }
        public string? Nome { get; set; }
        public TipoProduto Tipo { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}