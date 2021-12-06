using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.GamePanels.Pixels;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.Ships
{
    public abstract class Ship : BaseVisualElement
    {
        public int Length { set; get; }
        public string Name { set; get; }

        public Ship()
        {
            VisulBoodies = new List<IBoody>();
        }

        public object GetMeView
        {
            get
            {
                StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Background = Brushes.Red };
                //foreach (var item in VisulBoodies)
                //    stackPanel.Children.Add(new Label { Background = item.Background, Width=20 });
                return stackPanel;
            }
        }

        public override void Move(int param_r, int param_c)
        {
            if (CheckMove(param_r, param_c))
            {
                this.Column = param_c;
                this.Row = param_r;
            }
        }
        public bool CheckMove(int param_r, int param_c)
        {
            //int[] rColumn = new int[2]
            //    {
            //        VisulBoodies.FirstOrDefault().Column + param_c,
            //        VisulBoodies.LastOrDefault().Column + param_c
            //    };
            //int[] rRow = new int[2]
            //     {
            //        VisulBoodies.FirstOrDefault().Row + param_r,
            //        VisulBoodies.LastOrDefault().Row + param_r,
            //     };

            //bool res = true;

            //foreach (int t in rColumn)
            //    if (!(t > -1 && t < VisualElementsModel.CONST_C))
            //        res = false;
            //foreach (int t in rRow)
            //    if (!(t > -1 && t < VisualElementsModel.CONST_R))
            //        res = false;

            return true;
        }

        public override void Rotate()
        {
            int temp = 0;
            temp = Row;
            Row = Column;
            Column = temp;

            temp = RowSpan;
            RowSpan = ColumnSpan;
            ColumnSpan = temp;

            //if (VisulBoodies.FirstOrDefault().Column == VisulBoodies.LastOrDefault().Column)//vertical
            //{
            //    if (VisulBoodies.FirstOrDefault().Column > VisualElementsModel.CONST_C / 2)
            //    {
            //        for (int i = 0; i < Length; i++) //left
            //        {
            //            VisulBoodies[i].Column = VisulBoodies[i].Column - i;
            //            VisulBoodies[i].Row = VisulBoodies.FirstOrDefault().Row;
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < Length; i++) //right
            //        {
            //            VisulBoodies[i].Column = VisulBoodies[i].Column + i;
            //            VisulBoodies[i].Row = VisulBoodies.FirstOrDefault().Row;
            //        }
            //    }
            //}
            //else
            //{
            //    if (VisulBoodies.FirstOrDefault().Row > VisualElementsModel.CONST_R / 2) //up
            //    {
            //        for (int i = 0; i < Length; i++)
            //        {
            //            VisulBoodies[i].Row = VisulBoodies[i].Row - i;
            //            VisulBoodies[i].Column = VisulBoodies.FirstOrDefault().Column;
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < Length; i++)
            //        {
            //            VisulBoodies[i].Row = VisulBoodies[i].Row + i;
            //            VisulBoodies[i].Column = VisulBoodies.FirstOrDefault().Column;
            //        }
            //    }
            //}
        }

        public override void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            if (moved is null) return;
            moved.Move(Row, Column);
        }
       
        public override void IVisible_MouseEnter(object sender, MouseEventArgs e) 
        {
            PopupIsOpen = Visibility.Visible;
        }
        public override void IVisible_MouseLeave(object sender, MouseEventArgs e) 
        {
            PopupIsOpen = Visibility.Collapsed;
        }

        public override void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject();
            data.SetData("Object", this);

            DragDrop.DoDragDrop(sender as DependencyObject, data, DragDropEffects.All);
        }

    }
}
