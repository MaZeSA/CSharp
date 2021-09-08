using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityForm.BLL.Model
{
   public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public PositionDTO Position  { get; set; }
        public List<TaskDTO> Tasks { get; set; }
      //  public int PositionId { get; set; }
        public string GetDispay => $"ID[{Id}] {Name} {Surname} Age[{Age}] Salary[{Salary}]";

    }
}
