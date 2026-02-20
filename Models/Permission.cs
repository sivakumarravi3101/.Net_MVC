using System;

namespace WebApplication1.Models
{
    public class Permission
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime PermissionDate { get; set; }

        public int HoursOfPermission { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
