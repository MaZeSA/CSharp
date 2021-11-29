using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.GamePanels.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel.Interfaces
{
    public interface IVisible
    {
        List<IBoody> VisulBoodies { get;}

        void Move(int param_r, int param_c);
        void Rotate();

    }
}
