using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBackend.Data.Entities
{
    public class AplicattionUser : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(150)]
        public string Photo { get; set; }

    }
}
