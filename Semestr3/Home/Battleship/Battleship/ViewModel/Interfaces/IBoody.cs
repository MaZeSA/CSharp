using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel.Interfaces
{
    public interface IBoody
    {
        int Column { set; get; }
        int Row { set; get; }
        object Content { set; get; }
    }
}
