using EntityForm.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityForm.DAL.Initializer
{
    public class EmployeeInitializer : DropCreateDatabaseAlways<EntityDALContext>
    {
        protected override void Seed(EntityDALContext context)
        {
            var position = new List<Position>
            {
                new Position{Title = "Tester"},
                new Position{Title = "Sleeper"},
                new Position{Title = "Manager"}
            };

            var task = new List<Entities.Task>
            {
                new Entities.Task{Title = "Working", Priority = 0},
                new Entities.Task{Title = "Testing", Priority = 2},  
                new Entities.Task{Title = "Sleep", Priority = 10}
            };

            var employees = new List<Employee>
            {
                new Employee
                {
                    Name = "Yarik",
                    Surname = "Vodila",
                    Age = 23,
                    Salary = 3453.67,
                    Tasks = task.Where(x=> x.Title == "Sleep" ||x.Title ==  "Working").ToList(),
                    Position = position.FirstOrDefault(x=> x.Title == "Sleeper")
                }
            };

            context.Tasks.AddRange(task);
            context.Positions.AddRange(position);
            context.Employees.AddRange(employees);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
