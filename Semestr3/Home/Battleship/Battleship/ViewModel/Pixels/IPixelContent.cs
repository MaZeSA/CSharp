using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel.Pixels
{
    public interface IPixelContent
    {
        object Content(int x, int y);
    }
}
