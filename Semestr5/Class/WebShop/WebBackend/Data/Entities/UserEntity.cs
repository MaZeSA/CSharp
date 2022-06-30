using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBackend.Data.Entities
{
    [Table("tblUsers")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }
        public string Photo { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
