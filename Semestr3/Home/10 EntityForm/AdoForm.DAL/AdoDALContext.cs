using AdoForm.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AdoForm.DAL
{
    public class AdoDALContext : DbContext
    { 
        public AdoDALContext()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
    }
}
