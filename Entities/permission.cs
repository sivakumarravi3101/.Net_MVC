using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("permission_date")]
        public DateTime PermissionDate { get; set; }
        [Column("hours_of_permission")]
        public int HoursOfPermission { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
