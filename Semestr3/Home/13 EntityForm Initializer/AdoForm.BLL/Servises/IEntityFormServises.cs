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


        void AddPosition(PositionDTO positionDTO);
        void RemovePosition(PositionDTO positionDTO);
        void UpdatePosition(PositionDTO positionDTO);

        IEnumerable<PositionDTO> GetPositions();

        void AddTask(TaskDTO taskDTO);
        void RemoveTask(TaskDTO taskDTO);
        void UpdateTask(TaskDTO taskDTO);
        IEnumerable<TaskDTO> GetTasks();
    }
}
