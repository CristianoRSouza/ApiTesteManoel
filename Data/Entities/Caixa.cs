namespace LojaManoelApi.Data.Entities
{
    public class Caixa
    {
        public string CaixaId { get; set; }
        public Dimensao Dimensoes { get; set; }
        public double Volume => Dimensoes.Volume;
        public double VolumeDisponivel { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
