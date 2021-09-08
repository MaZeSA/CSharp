using EntityForm.DAL.Entities;
using System.Data.Entity;

namespace EntityForm.DAL
{
    public class EntityDALContext : DbContext
    { 
        public EntityDALContext()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
    }
}
