using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityForm.DAL.Entities
{
    public class Task
    {
        public Task()
        {
            Employees = new List<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        //[MinLength(5)]
        [Required]
        //[EmailAddress]
        public string Title { get; set; }
        public int? Priority { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
