using System;
using System.Data.Entity;
using System.Linq;

namespace EntityGames
{
    public class FirstContext : DbContext
    {
        public FirstContext()
            : base("name=FirstContext")
        {

        }
         public virtual DbSet<Games> Games { get; set; }
    }
}

