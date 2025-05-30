using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.Data.Dtos
{
    public class ProdutoDto
    {
        public string ProdutoId { get; set; }
        public DimensaoDto Dimensao { get; set; }
    }
}
