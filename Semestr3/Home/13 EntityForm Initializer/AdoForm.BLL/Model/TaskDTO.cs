using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityForm.BLL.Model
{
   public class TaskDTO
    {
        public int Id { set; get; }
        public string Title { get; set; }
        public int? Priority { get; set; }
        public enum Event
        {
            Add,
            Update,
            Remove
        }
    }
}
