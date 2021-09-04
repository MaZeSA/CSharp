using AdoForm.BLL.Model;
using AdoForm.DAL.Entities;
using AdoForm.DAL.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdoForm.BLL.Servises
{
    public class AdoFormServises : IAdoFormServises
    {
        private readonly IGenericRepository<Employee> repositoryEmployee; 
        private readonly IGenericRepository<Position> repositoryPosition;
        private readonly IGenericRepository<Task> repositoryTask;
        private readonly IMapper mapper;

        public AdoFormServises(IGenericRepository<Employee> _repositoryEmployee,
                               IGenericRepository<Position> _repositoryPosition,
                               IGenericRepository<Task> _repositoryTask,
                               IMapper _mapper)
        {
            repositoryEmployee = _repositoryEmployee;
            repositoryPosition = _repositoryPosition;
            repositoryTask = _repositoryTask;
            mapper = _mapper;
        }

        public void AddEmployee(EmployeeDTO _employee)
        {
            Employee employee = mapper.Map<Employee>(_employee);
            var position = repositoryPosition.GetAll().FirstOrDefault(x => x.Title == _employee.Position.Title);
            var task = repositoryTask.GetAll().Where(x => _employee.Tasks.FirstOrDefault(t => t.Title == x.Title)?.Title == x.Title);

            employee.Position = position;
            employee.Tasks = task.ToList();
            repositoryEmployee.Create(employee);
        }

        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            var employees = repositoryEmployee.GetAll();
            ICollection<EmployeeDTO> model = mapper.Map<ICollection<EmployeeDTO>>(employees);
            return model;
        }

        public void RemoveEmployee(EmployeeDTO employeeDTO)
        {
            var employee = repositoryEmployee.GetAll().FirstOrDefault(x => x.Id == employeeDTO.Id);
            repositoryEmployee.Delete(employee);
        }
        public void UpdateEmployee(EmployeeDTO _employee)
        {
            var employee = repositoryEmployee.GetAll().FirstOrDefault(x => x.Id == _employee.Id);
            Employee employeeNew = mapper.Map<Employee>(_employee);
            employee.Name = employeeNew.Name;
            employee.Surname = employeeNew.Surname;
            employee.Age = employeeNew.Age;
            employee.Salary = employeeNew.Salary;
            employee.Position = repositoryPosition.GetAll().FirstOrDefault(x=> x.Title == _employee.Position.Title);
            employee.Tasks = repositoryTask.GetAll().Where(x => _employee.Tasks.FirstOrDefault(t => t.Title == x.Title)?.Title == x.Title).ToList();

            repositoryEmployee.Update(employee);
        }

        public IEnumerable<PositionDTO>GetPositions()
        {
            var positions = repositoryPosition.GetAll();
            ICollection<PositionDTO> model = mapper.Map<ICollection<PositionDTO>>(positions);
            return model;
        }

        public IEnumerable<TaskDTO>GetTasks()
        {
            var tasks = repositoryTask.GetAll();
            ICollection<TaskDTO> model = mapper.Map<ICollection<TaskDTO>>(tasks);
            return model;
        }

      
    }
}
