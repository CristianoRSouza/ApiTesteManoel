using LojaManoelApi.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaManoelApi.Data.Entities
{
    public class Papel
    {
        public int Id { get; set; }
        public string PapelToken { get; set; }

        [NotMapped]
        public RoleType PapelEnum
        {
            get => Enum.Parse<RoleType>(PapelToken);
            set => PapelToken = value.ToString();
        }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
