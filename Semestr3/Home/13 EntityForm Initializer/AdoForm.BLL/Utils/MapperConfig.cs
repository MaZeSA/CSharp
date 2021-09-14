using AutoMapper;
using EntityForm.BLL.Model;
using EntityForm.DAL.Entities;
using System.Collections.Generic;

namespace EntityForm.BLL.Utils
{
   public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Employee, EmployeeDTO>()
                    .ForMember(x => x.Position, opt => opt.MapFrom(x => new PositionDTO { Title = x.Position.Title }));
            //  .ForMember(x => x.Tasks, opt => opt.MapFrom(x => x.Tasks));

            CreateMap<EmployeeDTO, Employee>()
                    .ForMember(x => x.Position, opt => opt.MapFrom(x => new Position { Title = x.Position.Title }));
                    //.ForMember(x => x.Tasks, opt => opt.MapFrom(x => x.Tasks));

            CreateMap<Position, PositionDTO>();
            CreateMap<PositionDTO, Position>();

            CreateMap<Task, TaskDTO>();
            CreateMap<TaskDTO, Task>();
        }

        //ICollection<Task> GetTasks(string[] task)
        //{
        //    var list = new List<Task>();
        //    foreach(var t in task)
        //    {
        //        list.Add(new Task { Title = t });
        //    }
        //    return list;
        //}
    }
}
