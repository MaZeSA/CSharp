using EntityForm.BLL.Model;
using System.Collections.Generic;

namespace EntityForm.BLL.Servises
{
    public interface IEntityFormServises
    {
        IEnumerable<EmployeeDTO> GetEmployees();
        void AddEmployee(EmployeeDTO employee);
        void RemoveEmployee(EmployeeDTO employeeDTO);
        void UpdateEmployee(EmployeeDTO employeeDTO);

        IEnumerable<PositionDTO> GetPositions(); 
        IEnumerable<TaskDTO> GetTasks();
    }
}
