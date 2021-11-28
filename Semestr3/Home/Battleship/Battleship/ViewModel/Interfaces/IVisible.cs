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
        BaseViewModel ViewModel { set; get; }

        IBoody GetBoody(Pixel pixel);
        void Add();
        void Remove();
        void Move(int param_r, int param_c);
        void Rotate();
        void ChangeParent(BaseViewModel baseViewModel);

    }
}
