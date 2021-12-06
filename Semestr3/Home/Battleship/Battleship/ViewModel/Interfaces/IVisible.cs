using Battleship.Commands;
using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.GamePanels.Pixels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Battleship.ViewModel.Interfaces
{
    public interface IVisible
    {
        VisualElementsModel VisualElementsModel { get; set; }
        CommandIVisibleRotate CommandIVisibleRotate { get; set; }

        List<IBoody> VisulBoodies { get;}

        int Column { get; set; }
        int Row { get; set; }
        int ColumnSpan { get; set; }
        int RowSpan { get; set; }
        Visibility WrongBorder { get; set; }
        Visibility PopupIsOpen { get; set; }
        SolidColorBrush BackgroundBrush { set; get; }
        SolidColorBrush BorderBrush { set; get; }
        Thickness BorderThickness { set; get; }

        string TestString { set; get; }

        void Move(int param_r, int param_c);
        bool CheckMove(IVisible obj);
        void Rotate();

    }
}
