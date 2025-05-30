using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.Data.Dtos
{
    public class CaixaDto
    {
        public string CaixaId { get; set; }
        public DimensaoDto Dimensoes { get; set; }
        public double Volume => Dimensoes.Volume;
        public double VolumeDisponivel { get; set; }
        public List<ProdutoDto> Produtos { get; set; } = new List<ProdutoDto>();
    }
}
