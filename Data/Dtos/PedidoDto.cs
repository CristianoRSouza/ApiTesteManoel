namespace LojaManoelApi.Data.Dtos
{
    public class PedidoDto
    {
        public int PedidoId { get; set; }

        public List<ProdutoDto> Produtos { get; set; } = new();
    }
}
