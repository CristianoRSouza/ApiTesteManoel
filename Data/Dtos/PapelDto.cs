using LojaManoelApi.Data.Entities;
using LojaManoelApi.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaManoelApi.Data.Dtos
{
    public class PapelDto
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}
