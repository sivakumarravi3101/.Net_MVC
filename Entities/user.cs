using System.ComponentModel.DataAnnotations.Schema;

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

        public ICollection<Permission> Permissions { get; set; }
    }
}
