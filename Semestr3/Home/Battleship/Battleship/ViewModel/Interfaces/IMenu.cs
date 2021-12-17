using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel.Interfaces
{
    public interface IMenu: INotifyPropertyChanged
    {
        void Show();
        void Back();
    }
}
