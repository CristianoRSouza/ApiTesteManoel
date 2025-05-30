using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaManoelApi.Data.Entities
{
    public class Produto
    {
        [Key]
        public string ProdutoId { get; set; }  
        public Dimensao Dimensao { get; set; }

        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
