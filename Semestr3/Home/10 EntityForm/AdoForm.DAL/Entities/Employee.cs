using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoForm.DAL.Entities
{
   public class Employee
    {
        public Employee()
        {
            Tasks = new List<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }

        // Навігаційні властивості
        public virtual Position Position { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
