﻿using LojaManoelApi.Data.Dtos;

namespace LojaManoelApi.Data.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Papel Papel { get; set; }
    }
}
