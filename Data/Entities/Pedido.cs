using System.ComponentModel.DataAnnotations;

namespace LojaManoelApi.Data.Entities
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        public List<Produto> Produtos { get; set; } = new();
    }

}
