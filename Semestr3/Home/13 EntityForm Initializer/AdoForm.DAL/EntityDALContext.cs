using EntityForm.DAL.Entities;
using EntityForm.DAL.Initializer;
using System.Data.Entity;

namespace EntityForm.DAL
{
    public class EntityDALContext : DbContext
    { 
        public EntityDALContext()
            : base("name=Model1")
        {
            Database.SetInitializer(new EmployeeInitializer());
        }

        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
    }
}
