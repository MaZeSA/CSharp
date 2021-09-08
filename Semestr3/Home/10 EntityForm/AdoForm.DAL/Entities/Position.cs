using System.Collections.Generic;

namespace EntityForm.DAL.Entities
{
    public class Position
    {
        public Position()
        {
            Employees = new List<Employee>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
