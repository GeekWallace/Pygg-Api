using System.ComponentModel.DataAnnotations;

namespace PyggApi.Models
{
    public class MemberRole
    {
        [Key]
        public int RoleId { get; set; }

        public int GroupId { get; set; }

        [MaxLength(100)]
        public string RoleName { get; set; }

        public byte[] RoleDescription { get; set; }
    }
}
