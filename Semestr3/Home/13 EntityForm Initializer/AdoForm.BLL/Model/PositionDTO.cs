using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityForm.BLL.Model
{
   public class PositionDTO
    {
        public int Id { set; get; }
        public string Title { get; set; }

        public enum Event
        {
            Add,
            Update,
            Remove
        }
    }
}
