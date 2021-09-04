using AdoForm.BLL.Model;
using AdoForm.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdoForm.BLL.Servises
{
    public interface IAdoFormServises
    {
        IEnumerable<EmployeeDTO> GetEmployees();
        void AddEmployee(EmployeeDTO employee);
        void RemoveEmployee(EmployeeDTO employeeDTO);
        void UpdateEmployee(EmployeeDTO employeeDTO);

        IEnumerable<PositionDTO> GetPositions(); 
        IEnumerable<TaskDTO> GetTasks();
    }
}
