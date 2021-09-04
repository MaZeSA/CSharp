using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoForm.BLL.Model
{
   public class TaskDTO
    {
        public int Id { set; get; }
        public string Title { get; set; }
        public int? Priority { get; set; }

    }
}
