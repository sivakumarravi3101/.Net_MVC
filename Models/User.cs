using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
