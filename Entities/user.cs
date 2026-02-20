using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Column("password_salt")]
        public string PasswordSalt { get; set; }
        [Column("last_logged_in")]
        public DateTime? LastLoggedIn { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("role")]
        public string Role { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
