using Battleship.Commands;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels.Pixels
{
    public class EntityPixel : BaseVisualElement, IVisible
    {
        public List<IBoody> VisulBoodies { set; get; } = new List<IBoody>();
        public PreviewDragEnterCommand PreviewDragEnterCommand { set; get; } = new PreviewDragEnterCommand();

        public EntityPixel(int row, int colum) 
        {
            Row = row;
            Column = colum;
            BorderBrush = Brushes.Black;
            BackgroundBrush = Brushes.Gray;
            BorderThickness = new Thickness(1);
            VisulBoodies.Add(new Pixel(this) { BackgroundBrush = this.BackgroundBrush }); 
        }

        public void Move(int param_r, int param_c)
        {
            
        }

        public void Rotate()
        {
           
        }

        public void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //dynamic t = e.OriginalSource;
            //IBoody rw = t.DataContext;
            //rw.ParentObj.Rotate();
        }

        public void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            if (moved is null) return;
        
        }

    }
}
