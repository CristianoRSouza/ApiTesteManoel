using System.ComponentModel.DataAnnotations;

namespace LojaManoelApi.Data.Entities
{
    public class Dimensao
    {
        [Key]
        public int Id { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }

        public double Volume => Altura * Largura * Comprimento;

    }
}
