using System.Windows;
using System.Windows.Media;

namespace Battleship.ViewModel.Interfaces
{
    public interface IBoody
    {
        int Column { set; get; }
        int Row { set; get; }
        object Content {get; }
        IVisible ParentObj { get; }
        SolidColorBrush BackgroundBrush { set; get; }
        SolidColorBrush BorderBrush { set; get; }
        Thickness BorderThickness { set; get; }
        bool ShotShow { set; get; }

        void UIElement_OnDragEnter(object sender, DragEventArgs e);
    }
}
